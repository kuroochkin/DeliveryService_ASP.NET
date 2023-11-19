using DeliveryService.App.Common.Errors;
using DeliveryService.Services.RestaurantAPI.App.Common.Interfaces;
using DeliveryService.Services.RestaurantAPI.App.Common.Messages;
using DeliveryService.Services.RestaurantAPI.App.Common.RabbitMQSender.Interfases;
using ErrorOr;
using MediatR;

namespace DeliveryService.Services.RestaurantAPI.App.Manager.Commands.CompleteOrder;

public class CompleteOrderCommandHandler
	: IRequestHandler<CompleteOrderRestaurantCommand, ErrorOr<bool>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IRabbitMQCompleteOrderByRestaurantSender _rabbitMqSender;

	public CompleteOrderCommandHandler(
		IUnitOfWork unitOfWork, 
		IRabbitMQCompleteOrderByRestaurantSender rabbitMqSender)
	{
		_unitOfWork = unitOfWork;
		_rabbitMqSender = rabbitMqSender;
	}

	public async Task<ErrorOr<bool>> Handle(
		CompleteOrderRestaurantCommand request,
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

		var completeOrderMessage = new ChangeOrderStatusDTO
		{
			OrderId = request.OrderId
		};

		//Отправляем сообщение об изменении статуса заказа в сервис заказов
		_rabbitMqSender.SendMessage(completeOrderMessage, "RestaurantAPI: CompleteOrderByRestaurant");

		return true;
	}
}
