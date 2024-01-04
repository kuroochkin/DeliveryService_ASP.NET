namespace DeliveryService.Services.CourierAPI.Contracts.Courier.Get;

public record GetCourierDetailsResponse(
    string Id,
    string Email,
    string UserName,
    string LastName,
    string FirstName,
    DateTime BirthDay,
    int CountOrder,
    string PhoneNumber
    );
