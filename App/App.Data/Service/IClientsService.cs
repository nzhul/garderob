using App.Models;
using System;
using System.Collections.Generic;
using System.Web;

namespace App.Data.Service
{
	public interface IClientsService
	{
		IEnumerable<ApplicationUser> GetUsers();

		ApplicationUser GetUserById(string id);

		void UpdateClient(ApplicationUser user);

		byte[] UploadProfileImage(HttpPostedFile uploadedImage, string userId);
	}
}
