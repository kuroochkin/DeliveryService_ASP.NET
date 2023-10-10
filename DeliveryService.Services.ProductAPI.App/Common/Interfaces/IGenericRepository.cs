namespace DeliveryService.Services.ProductAPI.App.Common.Interfaces;

public interface IGenericRepository<T>
	where T : class
{
	Task<T?> FindById(Guid id);
	Task<IEnumerable<T>> GetAll();
	Task<bool> Add(T entity);
	bool Delete(T entity);
	bool Update(T entity);
}
