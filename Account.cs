using System;
namespace SecureLogin
{
	public class Account
	{
		public string Username { get; set; }
		public string Password { get; set; }

		/// <summary>
		/// Basic constructor for a user account
		/// </summary>
		/// <param name="username">string representation of account username</param>
		/// <param name="password">string representation of account password</param>
		public Account(string username, string password)
		{
			Username = username;
			Password = password;
		}
	}
}

