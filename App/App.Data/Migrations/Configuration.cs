namespace App.Data.Migrations
{
	using App.Models;
	using App.Models.Pages;
	using Microsoft.AspNet.Identity;
	using Microsoft.AspNet.Identity.EntityFramework;
	using System;
	using System.Collections.Generic;
	using System.Configuration;
	using System.Data.Entity.Migrations;
	using System.IO;
	using System.Linq;
	using System.Web;

	public sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
	{
		private const string adminFirstNameConfigKey = "adminFirstname";
		private const string adminLastNameConfigKey = "adminLastname";
		private const string adminEmailConfigKey = "adminEmail";
		private const string adminPasswordConfigKey = "adminPassword";
		private const string adminPhoneConfigKey = "adminPhone";

		static Random rand = new Random();
		public Configuration()
		{
			AutomaticMigrationsEnabled = true;
			AutomaticMigrationDataLossAllowed = true;
		}

		protected override void Seed(ApplicationDbContext context)
		{
			this.AddInitialStaticPages(context);
			this.InitializeAdministrator(context);
		}

		private void InitializeAdministrator(ApplicationDbContext context)
		{
			if (!context.Users.Any())
			{
				IdentityRole adminRole = new IdentityRole { Name = "Admin", Id = Guid.NewGuid().ToString() };
				IdentityRole userRole = new IdentityRole { Name = "User", Id = Guid.NewGuid().ToString() };
				context.Roles.Add(adminRole);
				context.Roles.Add(userRole);

				// Initialize default user
				var userStore = new UserStore<ApplicationUser>(context);
				var userManager = new UserManager<ApplicationUser>(userStore);
				AdminConfiguration config = this.GetAdminConfiguration();

				ApplicationUser admin = new ApplicationUser();
				admin.UserName = config.Email;
				admin.FirstName = config.Firstname;
				admin.LastName = config.Lastname;
				admin.Email = config.Email;
				admin.PhoneNumber = config.Phone;

				userManager.Create(admin, config.Password);
				admin.Roles.Add(new IdentityUserRole { RoleId = adminRole.Id, UserId = admin.Id });
				admin.Roles.Add(new IdentityUserRole { RoleId = userRole.Id, UserId = admin.Id });
				context.SaveChanges();
			}
		}

		private AdminConfiguration GetAdminConfiguration()
		{
			AdminConfiguration config = new AdminConfiguration();
			config.Firstname = ConfigurationManager.AppSettings[Configuration.adminFirstNameConfigKey];
			config.Lastname = ConfigurationManager.AppSettings[Configuration.adminLastNameConfigKey];
			config.Email = ConfigurationManager.AppSettings[Configuration.adminEmailConfigKey];
			config.Password = ConfigurationManager.AppSettings[Configuration.adminPasswordConfigKey];
			config.Phone = ConfigurationManager.AppSettings[Configuration.adminPhoneConfigKey];

			return config;
		}

		private void AddInitialStaticPages(ApplicationDbContext context)
		{
			string folderPath = HttpContext.Current.Server.MapPath("~/App_Data/StaticPages");
			IEnumerable<string> pages = Directory.EnumerateFiles(folderPath);

			foreach (var pagePath in pages)
			{
				string fileName = Path.GetFileNameWithoutExtension(pagePath);

				if (!this.PageExistsInDatabase(context, fileName))
				{
					string fileContents = File.ReadAllText(pagePath);

					Page newPage = new Page
					{
						Content = fileContents,
						Title = fileName,
						UrlName = fileName,
						DateCreated = DateTime.UtcNow
					};

					context.Pages.Add(newPage);
				}
			}

			context.SaveChanges();
		}

		private bool PageExistsInDatabase(ApplicationDbContext context, string fileName)
		{
			if (context.Pages.Any(p => p.UrlName == fileName))
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		private class AdminConfiguration
		{
			public string Password { get; set; }

			public string Firstname { get; set; }

			public string Lastname { get; set; }

			public string Email { get; set; }

			public string Phone { get; set; }
		}
	}
}
