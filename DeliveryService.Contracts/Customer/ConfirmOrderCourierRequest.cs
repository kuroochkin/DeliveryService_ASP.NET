namespace DeliveryService.Contracts.Customer;

public record ConfirmOrderCourierRequest(
    string OrderId);

public record ConfirmOrderResponse(
    string OrderId);
