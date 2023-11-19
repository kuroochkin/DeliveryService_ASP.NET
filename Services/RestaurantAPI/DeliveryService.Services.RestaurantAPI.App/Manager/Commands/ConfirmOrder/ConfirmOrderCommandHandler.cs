using DeliveryService.Services.RestaurantAPI.App.Common.Errors;
using DeliveryService.Services.RestaurantAPI.App.Common.Interfaces;
using DeliveryService.Services.RestaurantAPI.App.Common.Messages;
using DeliveryService.Services.RestaurantAPI.App.Common.RabbitMQSender.Interfases;
using ErrorOr;
using MediatR;

namespace DeliveryService.Services.RestaurantAPI.App.Manager.Commands.ConfirmOrder;

public class ConfirmOrderRestaurantCommandHandler
	: IRequestHandler<ConfirmOrderRestaurantCommand, ErrorOr<bool>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IRabbitMQConfirmOrderByRestaurantSender _rabbitMqSender;

	public ConfirmOrderRestaurantCommandHandler(
		IUnitOfWork unitOfWork, 
		IRabbitMQConfirmOrderByRestaurantSender rabbitMqSender)
	{
		_unitOfWork = unitOfWork;
		_rabbitMqSender = rabbitMqSender;
	}

	public async Task<ErrorOr<bool>> Handle(
		ConfirmOrderRestaurantCommand request,
		CancellationToken cancellationToken)
	{
		if (!Guid.TryParse(request.ManagerId, out var managerId))
		{
			return Errors.Manager.InvalidId;
		}

		using (HttpClient httpClient = new HttpClient())
		{
			// Отправьте GET-запрос к другому микросервису
			HttpResponseMessage response = await httpClient.GetAsync($"http://host.docker.internal:5004/api/order/{request.OrderId}");

			// Проверьте, получен ли успешный ответ
			if (response.IsSuccessStatusCode)
			{
				// Прочитайте содержимое ответа и проверьте на null
				var isOrder = Convert.ToBoolean(await response.Content.ReadAsStringAsync());
				if (!isOrder)
					return false;
			}
			else
			{
				return false;
			}
		}

		var manager = await _unitOfWork.Managers.FindManagerWithRestaurantById(managerId);
		if (manager is null)
		{
			return Errors.Manager.NotFound;
		}

		var confirmOrderMessage = new ChangeOrderStatusAndStartManagerDTO
		{
			OrderId = request.OrderId,
			ManagerId = manager.Id.ToString(),
		};

		//Увеличиваем количество заказов у менеджера
		manager.AddOrder();

		//Отправляем сообщение об изменении статуса заказа в сервис заказов
		_rabbitMqSender.SendMessage(confirmOrderMessage, "RestaurantAPI: ConfirmOrderByRestaurant");

		return true;
	}
}
