namespace DeliveryService.Services.PaymentAPI.App.Common.Interfaces.Persistence;

public interface IGenericRepository<T>
	where T : class
{
	Task<T?> FindById(Guid id);
	Task<IEnumerable<T>> GetAll();
	Task<bool> Add(T entity);
	bool Delete(T entity);
	bool Update(T entity);
}
