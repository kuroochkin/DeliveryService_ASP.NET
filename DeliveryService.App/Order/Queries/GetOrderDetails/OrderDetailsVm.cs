using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.App.Order.Queries.GetOrderDetails
{
	public record OrderDetailsVm(
		string OrderId,
		string Description,
		DateTime Created,
		CourierVm Courier,
		CustomerVm Customer
		);

	public record CourierVm(
		string CourierId,
		string LastName,
		string FirstName);

	public record CustomerVm(
		string CustomerId,
		string LastName,
		string FirstName);
}
