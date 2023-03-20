
namespace DeliveryService.Domain.Courier;

public class CourierEntity
{
	public Guid Id { get;}

	public string LastName { get; }

	public string FirstName { get; }

	public string? Patronymic { get; }

	public DateTime BirthDay { get; }

	public int CountOrder { get; set; }

	public CourierEntity(Guid id, string firstName, string lastName)
	{
		Id = id;
		FirstName = firstName;
		LastName = lastName;
	}

	public CourierEntity()
	{

	}



	public string GetFullName()
	{
		return Patronymic is null ? $"{LastName} {FirstName}" : $"{LastName} {FirstName} {Patronymic}";
	}

}
