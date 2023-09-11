﻿using DeliveryService.Domain.Order;
using DeliveryService.Domain.Restaraunt;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryService.Domain.Product;

public class ProductEntity
{
	public int Id { get; }

	public string Title { get; set; }

	public double Price { get; set; }

	public string Thumbnail { get; set; }

	[ForeignKey("RestaurantId")]
	public RestaurantEntity Restaurant { get; set; }

	public Guid RestaurantId { get; set; }

	public SectionEntity? Section { get; set; }

	public ProductEntity(int id, string title)
	{
		Id = id;
		Title = title;
	}

	public ProductEntity(int id, string name, double price, SectionEntity section)
	{
		Id = id;
		Title = name;
		Price = price;
		Section = section;
	}

	public ProductEntity()
	{

	}
}
