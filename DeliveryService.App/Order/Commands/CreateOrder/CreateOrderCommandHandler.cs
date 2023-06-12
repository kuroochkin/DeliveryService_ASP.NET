using AutoMapper.Execution;
using DeliveryService.App.Common.Errors;
using DeliveryService.App.Common.Interfaces.Persistence;
using DeliveryService.Domain;
using DeliveryService.Domain.Order;
using ErrorOr;
using MediatR;
using static DeliveryService.Domain.Order.OrderEntity;

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

		if (!Guid.TryParse(request.CustomerId, out var customerId))
		{
			return Errors.Customer.InvalidId;
		}

		var customer = await _unitOfWork.Customers.FindById(customerId);
		if (customer is null)
		{
			return Errors.Customer.NotFound;
		}

		var orderItems =  request.Products.Select(product => new OrderItemEntity(
			Convert.ToInt32(product.Count),
			double.Parse(product.TotalPrice),
			Convert.ToInt32(product.ProductId)
			)).ToList();


		var order = new OrderEntity()
		{
			Description = request.Description,
			Status = OrderStatus.Create,
			Customer = customer,
			OrderItems = orderItems
		};

	
		if(await _unitOfWork.Orders.Add(order))	
		{
			customer.AddOrder(order);
			
			return await _unitOfWork.CompleteAsync();
		}

		return false;
	}
}
