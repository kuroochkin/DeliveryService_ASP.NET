using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.Contracts.Order.Get;

public record GetOrdersCourierResponse(
	List<GetOrderDetailsResponse> Orders);
