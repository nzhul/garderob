using App.Data.Service.Abstraction;
using App.Data.Utilities;
using App.Models;
using App.Models.InputModels;
using AutoMapper;
using System.IO;
using System.Linq;
using System.Web;
using System;

namespace App.Data.Service.Implementation
{
	public class ClientsService : IClientsService
	{
		private readonly IUoWData Data;
		private const int defaultPageSize = 10;
		private const int defaultPage = 0;

		public ClientsService(IUoWData data)
		{
			this.Data = data;
		}

		public IQueryable<ApplicationUser> GetUsers(int? page, int? pagesize)
		{
			if (page == null || page < 0)
			{
				page = defaultPage;
			}

			if (pagesize == null || pagesize < 1)
			{
				pagesize = defaultPageSize;
			}

			IQueryable<ApplicationUser> users = this.Data.Users.All().Where(u => u.IsActive == true).OrderByDescending(u => u.Email);
			users = users.Skip(page.Value * pagesize.Value).Take(pagesize.Value);

			return users;
		}

		public IQueryable<ApplicationUser> GetInactiveUsers()
		{
			return this.Data.Users.All().Where(u => u.IsActive == false).OrderByDescending(u => u.Email);
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
			dbUser.JobTitle = user.JobTitle;

			if (user.ProfileImage != null && user.ProfileImage.Length > 0)
			{
				dbUser.ProfileImage = user.ProfileImage;
			}

			this.Data.SaveChanges();
		}

		public byte[] UploadProfileImage(HttpPostedFileBase uploadedImage, string userId)
		{
			ApplicationUser user = this.GetUserById(userId);
			user.ProfileImage = ImageUtilities.CropImage(uploadedImage, "width=150&height=150&crop=auto&format=jpg");

			this.UpdateClient(user);

			return user.ProfileImage;
		}

		public int GetUsersCount()
		{
			return this.Data.Users.All().Where(u => u.IsActive).Count();
		}

		public void DeactivateClient(string id)
		{
			ApplicationUser dbUser = this.Data.Users.Find(id);

			if (dbUser != null)
			{
				dbUser.IsActive = false;
				this.Data.SaveChanges();
			}
		}

		public void ActivateClient(string id)
		{
			ApplicationUser dbUser = this.Data.Users.Find(id);

			if (dbUser != null)
			{
				dbUser.IsActive = true;
				this.Data.SaveChanges();
			}
		}

		public bool ClientExists(string id)
		{
			if (string.IsNullOrEmpty(id))
			{
				return false;
			}
			else
			{
				return this.Data.Users.All().Any(u => u.Id == id);
			}
		}

		public bool UpdateClient(string id, EditClientInputModel inputModel)
		{
			ApplicationUser dbUser = this.Data.Users.Find(id);
			if (dbUser != null)
			{
				dbUser = Mapper.Map(inputModel, dbUser);

				if (inputModel.PostedNewProfilePhoto != null)
				{
					dbUser.ProfileImage = ImageUtilities.CropImage(inputModel.PostedNewProfilePhoto, "width=150&height=150&crop=auto&format=jpg");
				}

				this.Data.SaveChanges();
				return true;
			}
			else
			{
				return false;
			}
		}

		public bool DeleteUserPhoto(string id)
		{
			ApplicationUser dbUser = this.GetUserById(id);

			if (dbUser != null)
			{
				dbUser.ProfileImage = null;
				this.Data.SaveChanges();

				return true;
			}
			else
			{
				return false;
			}
		}
	}
}