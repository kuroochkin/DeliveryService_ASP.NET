namespace DeliveryService.Contracts.Courier;

public record ConfirmOrderCourierRequest(
    string OrderId);

public record ConfirmOrderResponse(
    string OrderId);
