using DeliveryService.App.Common.Errors;
using DeliveryService.App.Common.Interfaces.Persistence;
using DeliveryService.Domain.Order;
using ErrorOr;
using MediatR;

namespace DeliveryService.App.Courier.Commands.AddCourier.AddOrder;

public class CreateOrderCommandHandler
	: IRequestHandler<CreateOrderCommand, ErrorOr<bool>>
{
	private readonly IUnitOfWork _unitOfWork;

	public CreateOrderCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<ErrorOr<bool>> Handle(
		CreateOrderCommand request, 
		CancellationToken cancellationToken)
	{
		if (!Guid.TryParse(request.CourierId, out var courierId))
		{
			return Errors.Courier.InvalidId;
		}

		if (!Guid.TryParse(request.CustomerId, out var customerId))
		{
			return Errors.Customer.InvalidId;
		}

		var courier = await _unitOfWork.Couriers.FindById(courierId);
		if(courier is null)
		{
			return Errors.Courier.NotFound;
		}

		var customer = await _unitOfWork.Customers.FindById(customerId);
		if (customer is null)
		{
			return Errors.Customer.NotFound;
		}

		var order = new OrderEntity()
		{
			Courier = courier,
			Customer = customer,
			Description = request.Description,
		};

		if(await _unitOfWork.Orders.Add(order))
		{
			courier.AddOrder(order);
			customer.AddOrder(order);

			return await _unitOfWork.CompleteAsync();
		}

		return false;
	}
}
