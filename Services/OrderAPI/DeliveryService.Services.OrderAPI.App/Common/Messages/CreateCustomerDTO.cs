namespace DeliveryService.Services.OrderAPI.App.Common.Messages;

public class CreateCustomerDTO
{
    public string UserId { get; set; }
    public string RoleName { get; set; }
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string UserName { get; set; }
}
