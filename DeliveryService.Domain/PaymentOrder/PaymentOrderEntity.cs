namespace DeliveryService.Domain.PaymentOrder;

public class PaymentOrderEntity
{
	public Guid Id { get; set; }

	public string Card { get; set; }

	public decimal Price { get; set; }

	public DateTime PaymentDate { get; set;}

	public PaymentOrderEntity(string card, decimal price) 
	{
		Id = Guid.NewGuid();
		Card = card;
		Price = price;
		PaymentDate = DateTime.Now;
	}
}
