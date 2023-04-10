﻿using static DeliveryService.Domain.Order.OrderEntity;

namespace DeliveryService.App.Order.Queries.GetOrderDetails;

public record OrderDetailsVm(
	string OrderId,
	string Description,
	DateTime Created,
	DateTime End,
	OrderStatus Status,
	CourierVm? Courier,
	CustomerVm Customer
	);

public record CourierVm(
	string ?CourierId,
	string ?LastName,
	string ?FirstName);

public record CustomerVm(
	string CustomerId,
	string LastName,
	string FirstName);
