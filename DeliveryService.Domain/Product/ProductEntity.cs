using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Domain.Product
{
	public class ProductEntity
	{
		public int Id { get; }

		public string Title { get; set; }

		public double Price { get; set; }

		public string Thumbnail { get; set; }

		public ProductEntity(int id, string title)
		{
			Id = id;
			Title = title;
		}

		public ProductEntity(int id, string name, double price, string thumbnail)
		{
			Id = id;
			Title = name;
			Price = price;
			Thumbnail = thumbnail;
		}

		public ProductEntity()
		{

		}






	}
}
