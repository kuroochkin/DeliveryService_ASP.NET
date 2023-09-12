using DeliveryService.Domain.Role;

namespace DeliveryService.Domain.User;

public class UserEntity
{
	public Guid Id { get; set; }
	public string LastName { get; set; }

	public string FirstName { get; set; }

	public string Password { get; set; }

	public string Email { get; set; }

	public RoleEntity Role { get; set; }

	public UserEntity(
		string firstName, 
		string lastName, 
		string password,
		string email, 
		RoleEntity role)
	{
		Id = Guid.NewGuid();
		FirstName = firstName;
		LastName = lastName;
		Password = password;
		Email = email;
		Role = role;
	}

	public UserEntity()
	{

	}

	public string GetTypeUser => Role.Name;


	public string GetUserTypeToString()
	{
		if(Role.Name == "Customer")
			return "Customer";
		else if(Role.Name == "Courier")
			return "Courier";
		else if(Role.Name == "Manager")
			return "Manager";

		return "";
	}
}

