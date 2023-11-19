using DeliveryService.Services.OrderAPI.App.Common.Interfaces.Persistence;
using DeliveryService.Services.OrderAPI.Domain.Customer;

namespace DeliveryService.Services.OrderAPI.Infrastructure.Persistence.Repositories;

public class CustomerRepository : GenericRepository<CustomerEntity>, ICustomerRepository
{
	public CustomerRepository(ApplicationDbContext context) : base(context)
	{
	}
}
