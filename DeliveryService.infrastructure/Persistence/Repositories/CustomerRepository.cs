using DeliveryService.App.Common.Interfaces.Persistence;
using DeliveryService.Domain.Customer;

namespace DeliveryService.infrastructure.Persistence.Repositories;

public class CustomerRepository : GenericRepository<CustomerEntity>, ICustomerRepository
{
	public CustomerRepository(ApplicationDbContext context) : base(context)
	{
	}
}
