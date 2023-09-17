using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using DeliveryService.Domain.PaymentOrder;

namespace DeliveryService.infrastructure.Persistence.EntityTypeConfiguration;


public class PaymentOrderConfiguration : IEntityTypeConfiguration<PaymentOrderEntity>
{
	public void Configure(EntityTypeBuilder<PaymentOrderEntity> builder)
	{
		builder.ToTable("OrderPayments");
		builder.HasKey(pay => pay.Id);
		builder.Property(pay => pay.Card);
		builder.Property(pay => pay.Price);
		builder.Property(pay => pay.PaymentDate);
	}
}
