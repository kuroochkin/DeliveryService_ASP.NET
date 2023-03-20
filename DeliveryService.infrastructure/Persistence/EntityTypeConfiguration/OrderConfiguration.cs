using DeliveryService.Domain.Order;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.infrastructure.Persistence.EntityTypeConfiguration;

public class OrderConfiguration : IEntityTypeConfiguration<OrderEntity>
{
	public void Configure(EntityTypeBuilder<OrderEntity> builder)
	{
		builder.ToTable("Orders");

		builder.HasKey(order => order.Id);

		
		
		builder.Property(order => order.Created);
		builder.Property(order => order.End);

		builder.Property(order => order.Id)
		   .IsRequired()
		   .ValueGeneratedNever();
	}
}
