namespace DeliveryService.Contracts.Courier;

public record EndOrderCourierRequest(
    string OrderId);

public record CompleteOrderResponse(
    string OrderId);
