namespace DeliveryService.Services.CourierAPI.Domain.Courier;

public class CourierEntity
{
	public Guid Id { get; }

	public string LastName { get; }

	public string FirstName { get; }

	public string? Patronymic { get; }

	public DateTime BirthDay { get; }

	public int CountOrder { get; set; }

	public CourierEntity(Guid id, string lastName, string firstName)
	{
		Id = id;
		FirstName = firstName;
		LastName = lastName;
		BirthDay = DateTime.Now;
		CountOrder = 0;
	}

	public CourierEntity()
	{

	}

	public void AddOrder()
	{
		CountOrder++;
	}

	public string GetFullName()
	{
		return Patronymic is null ? $"{LastName} {FirstName}" : $"{LastName} {FirstName} {Patronymic}";
	}
}
