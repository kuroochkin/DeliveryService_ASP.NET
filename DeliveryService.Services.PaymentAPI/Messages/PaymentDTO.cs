﻿namespace DeliveryService.Services.PaymentAPI.Messages;

public class PaymentDTO
{
	public Guid Id { get; set; }
	public string OrderId { get; set; }
	public string UserId { get; set; }
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public string Email { get; set; }
	public string CardNumber { get; set; }
	public string CVV { get; set; }
	public string ExpiryMonthYear { get; set; }
	public double OrderTotalSum { get; set; }
	public int CartTotalItems { get; set; }
}
