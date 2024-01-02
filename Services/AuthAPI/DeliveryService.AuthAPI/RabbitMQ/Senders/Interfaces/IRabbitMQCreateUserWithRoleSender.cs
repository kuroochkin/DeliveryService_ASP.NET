using DeliveryService.App.Common.RabbitMQSender;

namespace DeliveryService.AuthAPI.RabbitMQ.Senders.Interfaces
{
    public interface IRabbitMQCreateUserWithRoleSender
    {
        void SendMessage(BaseMessage baseMessage, string queueName);
    }
}
