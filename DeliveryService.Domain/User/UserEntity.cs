using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryService.Domain.User;

public class UserEntity
{
	public Guid Id { get; set; }
	public string LastName { get; set; }

	public string FirstName { get; set; }

	public string Password { get; set; }

	public string Email { get; set; }

	public string PhoneNumber { get; set; }

	public string City { get; set; }

	public UserType Type { get;  }

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
			case UserType.Customer:
				return "Customer";
			case UserType.Courier:
				return "Courier";
		}

		return "";
	}
}


public enum UserType
{
	Customer,
	Courier
}

