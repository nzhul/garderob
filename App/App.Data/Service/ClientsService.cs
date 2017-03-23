using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Models;

namespace App.Data.Service
{
	public class ClientsService : IClientsServices
	{
		private readonly IUoWData Data;

		public ClientsService(IUoWData data)
		{
			this.Data = data;
		}

		public IEnumerable<ApplicationUser> GetUsers()
		{
			return this.Data.Users.All();
		}

		public ApplicationUser GetUserById(string id)
		{
			return this.Data.Users.Find(id);
		}

		public void UpdateClient(ApplicationUser user)
		{
			ApplicationUser dbUser = this.Data.Users.All().Single(u => u.Email == user.Email);
			dbUser.FirstName = user.FirstName;
			dbUser.LastName = user.LastName;
			dbUser.Company = user.Company;
			dbUser.City = user.City;
			dbUser.Address = user.Address;
			dbUser.DeliveryAddress = user.DeliveryAddress;
			dbUser.PhoneNumber = user.PhoneNumber;
			dbUser.InvoiceData = user.InvoiceData;

			this.Data.SaveChanges();
		}
	}
}
