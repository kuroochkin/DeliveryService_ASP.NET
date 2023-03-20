using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Domain.Product
{
	public class ProductEntity
	{
		public Guid Id { get; }

		public string Name { get; set; }
		public string? Description { get; set; }

		public int Count { get; set; }

		public ProductEntity(Guid id, string name, string description)
		{
			Id = id;
			Name = name;
			Description = description;
		}

		public ProductEntity(Guid id, string name)
		{
			Id = id;
			Name = name;
		}

		public ProductEntity()
		{

		}






	}
}
