using DeliveryService.Services.CourierAPI.App.Common.Errors;
using DeliveryService.Services.CourierAPI.App.Common.Interfaces.Persistence;
using DeliveryService.Services.CourierAPI.App.Common.Messages;
using DeliveryService.Services.CourierAPI.App.Common.RabbitMQSender.Interfaces;
using ErrorOr;
using MediatR;

namespace DeliveryService.Services.CourierAPI.App.Courier.Commands.ConfirmOrder;

public class ConfirmOrderCourierCommandHandler
	: IRequestHandler<ConfirmOrderCourierCommand, ErrorOr<bool>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IRabbitMQConfirmOrderByCourierSender _rabbitMqSender;

	public ConfirmOrderCourierCommandHandler(
		IUnitOfWork unitOfWork,
		IRabbitMQConfirmOrderByCourierSender rabbitMqSender)
	{
		_unitOfWork = unitOfWork;
		_rabbitMqSender = rabbitMqSender;
	}

	public async Task<ErrorOr<bool>> Handle(
		ConfirmOrderCourierCommand request,
		CancellationToken cancellationToken)
	{
		if (!Guid.TryParse(request.CourierId, out var courierId))
		{
			return Errors.Courier.InvalidId;
		}

		using (HttpClient httpClient = new HttpClient())
		{
			// Отправьте GET-запрос к другому микросервису
			HttpResponseMessage response = await httpClient.GetAsync($"http://host.docker.internal:5004/api/order/find/{request.OrderId}");

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

		var courier = await _unitOfWork.Couriers.FindById(courierId);
		if (courier is null)
		{
			return Errors.Courier.NotFound;
		}

		var confirmOrderMessage = new ChangeOrderStatusAndStartCourierDTO
		{
			OrderId = request.OrderId,
			CourierId = courier.Id.ToString(),
		};

		//Увеличиваем количество заказов у курьера
		courier.AddOrder();

		//Отправляем сообщение об изменении статуса заказа в сервис заказов
		_rabbitMqSender.SendMessage(confirmOrderMessage, "CourierAPI: ConfirmOrderByCourier");

		return true;
	}

	
}

