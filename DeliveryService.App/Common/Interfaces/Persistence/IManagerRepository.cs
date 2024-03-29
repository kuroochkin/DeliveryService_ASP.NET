﻿using DeliveryService.Domain.Manager;

namespace DeliveryService.App.Common.Interfaces.Persistence;

public interface IManagerRepository : IGenericRepository<ManagerEntity>
{
	Task<ManagerEntity?> FindManagerWithRestaurantById(Guid id);
}
