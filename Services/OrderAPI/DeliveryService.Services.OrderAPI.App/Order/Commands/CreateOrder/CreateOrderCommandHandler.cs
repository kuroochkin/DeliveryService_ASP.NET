using DeliveryService.Services.OrderAPI.App.Common.Interfaces.Persistence;
using DeliveryService.Services.OrderAPI.Domain.Order;
using DeliveryService.Services.OrderAPI.Domain.OrderItem;
using ErrorOr;
using MediatR;
using static DeliveryService.Services.OrderAPI.Domain.Order.OrderEntity;

namespace DeliveryService.Services.OrderAPI.App.Order.Commands.CreateOrder;

public class CreateOrderCommandHandler
	: IRequestHandler<CreateOrderCommand, ErrorOr<bool>>
{
	private readonly IUnitOfWork _unitOfWork;

	public CreateOrderCommandHandler(
		IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<ErrorOr<bool>> Handle(
		CreateOrderCommand request, 
		CancellationToken cancellationToken)
	{
		using (HttpClient httpClient = new HttpClient())
		{
			// Отправьте GET-запрос к другому микросервису
			HttpResponseMessage response = await httpClient.GetAsync($"http://host.docker.internal:5000/api/customer/{request.CustomerId}");

			// Проверьте, получен ли успешный ответ
			if (response.IsSuccessStatusCode)
			{
				// Прочитайте содержимое ответа и проверьте на null
				var isCustomer = Convert.ToBoolean(await response.Content.ReadAsStringAsync());
				if (!isCustomer)
					return false;
			}
			else
			{
				return false;
			}
		}

		var orderItems = request.Products.Select(product => new OrderItemEntity(
		   Convert.ToInt32(product.Count),
		   double.Parse(product.TotalPrice),
		   Convert.ToInt32(product.ProductId),
		   product.Thumbnail,
		   product.Title
		   )).ToList();

		var order = new OrderEntity()
		{
			Description = request.Description,
			Status = OrderStatus.Create,
			CustomerId = Guid.Parse(request.CustomerId),
			OrderItems = orderItems,
		};

		if (await _unitOfWork.Orders.Add(order))
		{
			// Тут добавить реализацию сообщения о том, что у заказчика увеличилось количество заказов
			return await _unitOfWork.CompleteAsync();
		}

		return false;
	}
}
