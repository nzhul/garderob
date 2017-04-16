using App.Models.Configs;

namespace App.Data.Service.Abstraction
{
	public interface IConfigService
	{
		AdminConfiguration GetAdminConfiguration();
	}
}