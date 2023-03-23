﻿namespace DeliveryService.Contracts.Order.Get;

public record GetOrderDetailsResponse(
	string OrderId,
	string Description,
	DateTime Created,
	CourierResponse Courier,
	CustomerResponse Customer
	);

public record CourierResponse(
	string CourierId,
	string LastName,
	string FirstName,
	string Patronymic);

public record CustomerResponse(
	string CustomerId,
	string LastName,
	string FirstName,
	string Patronymic);

