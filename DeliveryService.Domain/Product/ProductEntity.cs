﻿using DeliveryService.Domain.StorageFile;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryService.Domain.Product;

public class ProductEntity
{
	public int Id { get; }

	public string Title { get; set; }

	public double Price { get; set; }

	public StorageFileEntity StorageFile { get; set; } = new(); 

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
