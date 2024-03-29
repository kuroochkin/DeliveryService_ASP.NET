﻿using ErrorOr;
using MediatR;

namespace DeliveryService.App.Order.Commands.ConfirmOrderRestaurant;

public record ConfirmOrderRestaurantCommand(
		string ManagerId,
		string OrderId) : IRequest<ErrorOr<bool>>;
