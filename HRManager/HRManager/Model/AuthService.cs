using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManager.Model
{
	public class AuthService
	{
		private readonly IConfiguration _config;

		public AuthService(IConfiguration config)
		{
			_config = config;
		}

		public bool Login()
		{
			Console.Write("Username: ");
			var username = Console.ReadLine();
			Console.Write("Password: ");
			var password = Console.ReadLine();

			return username == _config["AdminCredentials:Username"] &&
				   password == _config["AdminCredentials:Password"];
		}
		public bool Authenticate(string username, string password)
		{
			var storedUsername = _config["AdminCredentials:Username"];
			var storedPassword = _config["AdminCredentials:Password"];

			return username == storedUsername && password == storedPassword;
		}
	}
}
