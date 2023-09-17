using System.Security.Cryptography;
using System.Text;

namespace DeliveryService.App.Auth;

public static class PasswordHasher
{
	public static string HashPassword(string password)
	{
		using (SHA256 sha256 = SHA256.Create())
		{
			byte[] bytes = Encoding.UTF8.GetBytes(password);
			byte[] hash = sha256.ComputeHash(bytes);
			return Convert.ToBase64String(hash);
		}
	}

	public static bool VerifyPassword(string password, string hashedPassword)
	{
		string hashedInputPassword = HashPassword(password);
		return string.Equals(hashedInputPassword, hashedPassword);
	}
}
