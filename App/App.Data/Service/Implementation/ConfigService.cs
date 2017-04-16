using App.Data.Service.Abstraction;
using App.Models.Configs;
using System.Configuration;

namespace App.Data.Service.Implementation
{
	public class ConfigService : IConfigService
	{
		private const string adminFirstNameConfigKey = "adminFirstname";
		private const string adminLastNameConfigKey = "adminLastname";
		private const string adminEmailConfigKey = "adminEmail";
		private const string adminPasswordConfigKey = "adminPassword";
		private const string adminPhoneConfigKey = "adminPhone";

		public AdminConfiguration GetAdminConfiguration()
		{
			AdminConfiguration config = new AdminConfiguration();
			config.Firstname = ConfigurationManager.AppSettings[ConfigService.adminFirstNameConfigKey];
			config.Lastname = ConfigurationManager.AppSettings[ConfigService.adminLastNameConfigKey];
			config.Email = ConfigurationManager.AppSettings[ConfigService.adminEmailConfigKey];
			config.Password = ConfigurationManager.AppSettings[ConfigService.adminPasswordConfigKey];
			config.Phone = ConfigurationManager.AppSettings[ConfigService.adminPhoneConfigKey];

			return config;
		}
	}
}
