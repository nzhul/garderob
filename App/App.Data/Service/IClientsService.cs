﻿using App.Models;
using System.Linq;
using System.Web;
using App.Models.InputModels;

namespace App.Data.Service
{
	public interface IClientsService
	{
		IQueryable<ApplicationUser> GetUsers(int? page, int? pagesize);

		IQueryable<ApplicationUser> GetInactiveUsers();

		ApplicationUser GetUserById(string id);

		void UpdateClient(ApplicationUser user);

		bool UpdateClient(string id, EditClientInputModel inputModel);

		byte[] UploadProfileImage(HttpPostedFile uploadedImage, string userId);

		int GetUsersCount();

		void DeactivateClient(string id);

		void ActivateClient(string id);

		bool ClientExists(string id);
	}
}
