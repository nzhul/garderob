using App.Models;
using System;
using System.Collections.Generic;

namespace App.Data.Service
{
	public interface IClientsServices
	{
		IEnumerable<ApplicationUser> GetUsers();

		ApplicationUser GetUserById(string id);
		void UpdateClient(ApplicationUser user);
	}
}
