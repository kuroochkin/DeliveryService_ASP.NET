using DeliveryService.Services.PaymentAPI.Domain.Payment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeliveryService.Services.PaymentAPI.Infrastructure.Persistence.EntityTypeConfiguration;

public class PaymentConfiguration : IEntityTypeConfiguration<PaymentEntity>
{
	public void Configure(EntityTypeBuilder<PaymentEntity> builder)
	{
		builder.HasKey(payment => payment.Id);

		builder.Property(payment => payment.OrderId);
		builder.Property(payment => payment.UserId);
		builder.Property(payment => payment.FirstName);
		builder.Property(payment => payment.LastName);
		builder.Property(payment => payment.Email);
		builder.Property(payment => payment.CardNumber);
		builder.Property(payment => payment.CVV);
		builder.Property(payment => payment.ExpiryMonthYear);
		builder.Property(payment => payment.CartTotalItems);
		builder.Property(payment => payment.OrderTotalSum);
	}
}
