using DeliveryService.Domain.Courier;
using DeliveryService.Domain.Customer;
using DeliveryService.Domain.Restaraunt;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryService.Domain.Order;

public class OrderEntity
{
	public Guid Id { get; }

	#region Date
	public DateTime Created { get; set; }

	public DateTime ConfirmedRestaurant { get; set; }

	public DateTime EndRestaurant { get; set; }

	public DateTime ConfirmedCourier { get; set; }

	public DateTime End { get; set; }

	#endregion

	public RestaurantEntity Restaurant { get; set; }
	public string Description { get; set; }

	public enum OrderStatus
	{
		Create,
		ConfirmedRestaurant,
		EndRestaurant,
		ConfirmedCourier,
		Complete,
	};

	[NotMapped]
	public OrderStatus Status { get; set; }
	public OrderStatus GetStatus => Status;

	public string GetStatusOrderToString()
	{
		switch (Status)
		{
			case OrderStatus.Create:
				return "Create";
			case OrderStatus.ConfirmedRestaurant:
				return "ConfirmedRestaurant";
			case OrderStatus.EndRestaurant:
				return "EndRestaurant";
			case OrderStatus.ConfirmedCourier:
				return "ConfirmedCourier";
			case OrderStatus.Complete:
				return "Complete";
		}

		return "";
	}

	public CourierEntity? Courier { get; set; }

	public CustomerEntity Customer { get; set; } = null!;

	public List<OrderItemEntity> OrderItems { get; set; } = new();

	public OrderEntity()
	{
		Id = Guid.NewGuid();
		Created = DateTime.Now;
	}
}
