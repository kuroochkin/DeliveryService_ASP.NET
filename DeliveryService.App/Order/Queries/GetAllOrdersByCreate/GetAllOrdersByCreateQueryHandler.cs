﻿using DeliveryService.App.Common.Interfaces.Persistence;
using DeliveryService.App.Order.Queries.GetOrderDetails;
using DeliveryService.App.Order.Queries.GetOrdersUser;
using ErrorOr;
using MediatR;


namespace DeliveryService.App.Order.Queries.GetAllOrdersByCreate
{
	public class GetAllOrdersByCreateQueryHandler
		: IRequestHandler<GetAllOrdersByCreateQuery, ErrorOr<OrdersUserVm>>
	{
		private readonly IUnitOfWork _unitOfWork;
		public GetAllOrdersByCreateQueryHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public async Task<ErrorOr<OrdersUserVm>> Handle(
			GetAllOrdersByCreateQuery request,
			CancellationToken cancellationToken)
		{
			var orders = await _unitOfWork.Orders.FindOrdersByCreate();

			var allOrderModel = orders.Select(order => new OrderDetailsVm(
		   order.Id.ToString(),
		   order.Description,
		   order.Created,
		   order.End,
		   order.Status,
		   new CourierVm(
			   order?.Courier?.Id.ToString(),
			   order?.Courier?.LastName,
			   order?.Courier?.FirstName
			   ),
		   new CustomerVm(
			   order.Customer.Id.ToString(),
			   order.Customer.LastName,
			   order.Customer.FirstName
			   ),
		   new List<ProductOrderVm>(
			   order.OrderItems.Select(product => new ProductOrderVm(
				   product.ProductId.ToString(),
				   product.Count.ToString(),
				   product.TotalPrice.ToString()
				   )).ToList()
		   ).ToList())).ToList();

			var allOrdersByCreate = new OrdersUserVm(allOrderModel);

			return allOrdersByCreate;
		}
	}
}
