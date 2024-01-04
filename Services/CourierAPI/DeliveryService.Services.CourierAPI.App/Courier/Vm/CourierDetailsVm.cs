namespace DeliveryService.Services.CourierAPI.App.Courier.Vm;

public record CourierDetailsVm(
    string Id,
    string Email,
    string UserName,
    string LastName,
    string FirstName,
    DateTime BirthDay,
    int CountOrder,
    string PhoneNumber
    );
