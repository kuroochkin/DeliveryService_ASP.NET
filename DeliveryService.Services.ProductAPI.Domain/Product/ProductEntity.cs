﻿using DeliveryService.Services.ProductAPI.Domain.Section;

namespace DeliveryService.Services.ProductAPI.Domain.Product;

public class ProductEntity
{
	public int Id { get; }

	public string Title { get; set; }

	public double Price { get; set; }

	public string Thumbnail { get; set; }

	public Guid RestaurantId { get; set; }

	public SectionEntity? Section { get; set; }

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
