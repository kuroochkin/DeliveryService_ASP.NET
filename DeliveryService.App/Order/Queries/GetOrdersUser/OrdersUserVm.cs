using DeliveryService.App.Order.Queries.GetOrderDetails;

namespace DeliveryService.App.Order.Queries.GetOrdersUser;

public record OrdersUserVm(List<OrderDetailsVm> Orders);