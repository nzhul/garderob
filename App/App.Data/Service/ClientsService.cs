using App.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace App.Data.Service
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

			IQueryable<ApplicationUser> users = this.Data.Users.All().OrderByDescending(u => u.Email);
			users = users.Skip(page.Value * pagesize.Value).Take(pagesize.Value);

			return users;
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

			if (user.ProfileImage != null && user.ProfileImage.Length > 0)
			{
				dbUser.ProfileImage = user.ProfileImage;
			}

			this.Data.SaveChanges();
		}

		public byte[] UploadProfileImage(HttpPostedFile uploadedImage, string userId)
		{
			MemoryStream stream = new MemoryStream();

			ImageResizer.ImageJob i = new ImageResizer.ImageJob(uploadedImage, stream, new ImageResizer.Instructions("width=150&height=150&crop=auto&format=jpg"));
			i.Build();

			ApplicationUser user = this.GetUserById(userId);
			user.ProfileImage = stream.ToArray();

			this.UpdateClient(user);

			return user.ProfileImage;
		}

		public int GetUsersCount()
		{
			return this.Data.Users.All().Count();
		}
	}
}
