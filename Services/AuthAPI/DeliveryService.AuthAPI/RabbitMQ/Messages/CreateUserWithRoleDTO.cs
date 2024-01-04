using DeliveryService.App.Common.RabbitMQSender;
namespace DeliveryService.AuthAPI.RabbitMQ.Messages;

public class CreateUserWithRoleDTO : BaseMessage
{
    public string UserId { get; set; }
    public string RoleName { get; set; }
    public string UserName { get; set; }
    public string LastName { get; set; }
    public string FirstName { get; set; }
}
