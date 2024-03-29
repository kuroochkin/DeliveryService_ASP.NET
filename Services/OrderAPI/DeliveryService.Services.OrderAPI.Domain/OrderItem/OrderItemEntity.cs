﻿namespace DeliveryService.Services.OrderAPI.Domain.OrderItem;

public class OrderItemEntity
{
	public Guid Id { get; set; }

	public int Count { get; set; }

	public double TotalPrice { get; set; }

	public string Title { get; set; }

	public string Thumbnail { get; set; }

	public int ProductId { get; set; }

	public OrderItemEntity(
		int count, 
		double totalPrice, 
		int productId, 
		string thumbnail, 
		string title)
	{
		Id = new Guid();
		Count = count;
		TotalPrice = totalPrice;
		ProductId = productId;
		Title = title;
		Thumbnail = thumbnail;
	}
}
