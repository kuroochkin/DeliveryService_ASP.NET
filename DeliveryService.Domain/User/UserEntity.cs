namespace DeliveryService.Domain.User;

public class UserEntity
{
	public Guid Id { get; set; }
	public string LastName { get; }

	public string FirstName { get; }

	public string Password { get; }

	public string Email { get; }

	public UserType Type { get; }

	public UserEntity(string firstName, string lastName, string password, string email, UserType userType)
	{
		Id = Guid.NewGuid();
		FirstName = firstName;
		LastName = lastName;
		Password = password;
		Email = email;
		Type = userType;
	}

	public UserEntity()
	{

	}

	public UserType GetTypeUser => Type;

	public string GetUserTypeToString()
	{
		switch (Type)
		{
			case UserType.Courier:
				return "courier";
			case UserType.Customer:
				return "customer";
		}

		return "";
	}
}

public enum UserType
{
	Courier,
	Customer
}

