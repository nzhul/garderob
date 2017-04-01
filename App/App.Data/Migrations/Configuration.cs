namespace App.Data.Migrations
{
	using App.Models;
	using App.Models.Images;
	using App.Models.Materials;
	using App.Models.Orders;
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

		public Configuration()
		{
			AutomaticMigrationsEnabled = true;
			AutomaticMigrationDataLossAllowed = true;
		}

		protected override void Seed(ApplicationDbContext context)
		{
			this.AddInitialStaticPages(context);
			this.InitializeAdministrator(context, true);
			this.InitializeMaterials(context);
			this.InitializeDummyOrders(context);
		}

		private void InitializeDummyOrders(ApplicationDbContext context)
		{
			if (!context.Orders.Any() && !context.OrderCategories.Any())
			{
				if (HttpContext.Current != null)
				{
					string ordersFolder = HttpContext.Current.Server.MapPath("~/App_Data/Orders");
					IEnumerable<string> subFolders = Directory.EnumerateDirectories(ordersFolder);

					foreach (string folderPath in subFolders)
					{
						string folderName = new DirectoryInfo(folderPath).Name;

						OrderCategory category = new OrderCategory
						{
							Name = folderName,
							Slug = folderName,
							Description = folderName + " description",
							DateCreated = DateTime.UtcNow,
							LastModified = DateTime.UtcNow
						};

						context.OrderCategories.Add(category);
						context.SaveChanges();

						string bigFolderPath = Path.Combine(folderPath, "Big");

						IEnumerable<string> bigImagePaths = Directory.EnumerateFiles(bigFolderPath);

						foreach (var imagePath in bigImagePaths)
						{
							string fileName = Path.GetFileNameWithoutExtension(imagePath);

							Order order = new Order
							{
								RequestDate = DateTime.UtcNow,
								CompleteDate = DateTime.UtcNow,
								OfferDate = DateTime.UtcNow,
								LastModified = DateTime.UtcNow,
								Title = fileName,
								Slug = fileName,
								Price = 0,
								State = OrderState.Done,
								OrderText = "-Empty-",
								OrderCategory = category,
								Client = null, // Get the admin user here
								BaseMaterial = null,
								DoorsMaterial = null,
								FazerMaterial = null,
								HandlesMaterial = null,
							};

							string bigImagePath = imagePath;
							string smallImagePath = imagePath.Replace("\\Big\\", "\\Small\\");
							byte[] bigImageData = this.LoadImageData(bigImagePath);
							byte[] smallImageData = this.LoadImageData(smallImagePath);

							order.ResultImages.Add(new Image { Big = bigImageData, Small = smallImageData });
							context.Orders.Add(order);
						}

						context.SaveChanges();
					}
				}

				// Seed some complete orders to use them in gallery page.
			}
		}

		private void InitializeMaterials(ApplicationDbContext context)
		{
			if (!context.Materials.Any() && !context.MaterialCategories.Any())
			{
				MaterialCategory otherCategory = new MaterialCategory
				{
					Name = "Други",
					Description = "In this category are placed all materials that do not belong in any other category.",
					Slug = "others",
					DateCreated = DateTime.UtcNow,
					LastModified = DateTime.UtcNow
				};

				MaterialCategory surfaceCategory = new MaterialCategory
				{
					Name = "Повърхности",
					Description = "Описание на категорията",
					Slug = "surfaces",
					DateCreated = DateTime.UtcNow,
					LastModified = DateTime.UtcNow
				};

				context.MaterialCategories.Add(otherCategory);
				context.MaterialCategories.Add(surfaceCategory);
				context.SaveChanges();

				this.GenerateMaterials(context);
				this.GenerateSurfaceMaterials(context, surfaceCategory.Id);
			}
		}

		private void GenerateMaterials(ApplicationDbContext context)
		{
			if (HttpContext.Current != null)
			{
				string materialsFolder = HttpContext.Current.Server.MapPath("~/App_Data/Materials");
				IEnumerable<string> subFolders = Directory.EnumerateDirectories(materialsFolder);

				foreach (string folderPath in subFolders)
				{
					string folderName = new DirectoryInfo(folderPath).Name;
					if (folderName == "Surfaces")
					{
						continue;
					}

					string bigFolderPath = Path.Combine(folderPath, "Big");

					// Create new material category based on folder name
					MaterialCategory category = new MaterialCategory
					{
						Name = folderName,
						Slug = folderName.ToLower(),
						Description = folderName + " description",
						DateCreated = DateTime.UtcNow,
						LastModified = DateTime.UtcNow
					};

					context.MaterialCategories.Add(category);
					context.SaveChanges();

					IEnumerable<string> bigImagePaths = Directory.EnumerateFiles(bigFolderPath);

					foreach (var imagePath in bigImagePaths)
					{
						string bigImagePath = imagePath;
						string smallImagePath = imagePath.Replace("\\Big\\", "\\Small\\"); // This could fail on the server!

						byte[] bigImageData = this.LoadImageData(bigImagePath);
						byte[] smallImageData = this.LoadImageData(smallImagePath);

						string fileName = Path.GetFileNameWithoutExtension(bigImagePath);

						Material material = new Material
						{
							CategoryId = category.Id,
							Name = fileName,
							Slug = fileName,
							Image = new Image { Big = bigImageData, Small = smallImageData },
							DateCreated = DateTime.UtcNow,
							LastModified = DateTime.UtcNow
						};

						context.Materials.Add(material);
					}

					context.SaveChanges();
				}
			}
		}

		private void GenerateSurfaceMaterials(ApplicationDbContext context, int surfaceCategoryId)
		{
			if (HttpContext.Current != null)
			{
				string bigImagesFolder = HttpContext.Current.Server.MapPath("~/App_Data/Materials/Surfaces/Big");
				IEnumerable<string> bigImages = Directory.EnumerateFiles(bigImagesFolder);

				foreach (string imagePath in bigImages)
				{
					string bigImagePath = imagePath;
					string smallImagePath = imagePath.Replace("\\Big\\", "\\Small\\"); // This could fail on the server!
					string liveFrontImagePath = imagePath.Replace("\\Big\\", "\\LivePreview\\Front\\"); // This could fail on the server!
					string liveBackImagePath = imagePath.Replace("\\Big\\", "\\LivePreview\\Back\\"); // This could fail on the server!

					byte[] bigImageData = this.LoadImageData(bigImagePath);
					byte[] smallImageData = this.LoadImageData(smallImagePath);
					byte[] liveFrontImageData = this.LoadImageData(liveFrontImagePath);
					byte[] livebackImageData = this.LoadImageData(liveBackImagePath);

					string fileName = Path.GetFileNameWithoutExtension(bigImagePath);

					SurfaceMaterial surfaceMaterial = new SurfaceMaterial
					{
						CategoryId = surfaceCategoryId,
						Name = fileName,
						Slug = fileName,
						Image = new Image { Big = bigImageData, Small = smallImageData },
						LivePreviewFrontImage = new Image { Big = liveFrontImageData },
						LivePreviewBackImage = new Image { Big = livebackImageData },
						DateCreated = DateTime.UtcNow,
						LastModified = DateTime.UtcNow
					};

					context.SurfaceMaterials.Add(surfaceMaterial);
				}

				context.SaveChanges();
			}
		}

		private void InitializeAdministrator(ApplicationDbContext context, bool generateDummyUsers)
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
				admin.ProfileImage = this.LoadImageData(HttpContext.Current.Server.MapPath("~/App_Data/AdminProfileImage/avatar.jpg"));
				admin.RegisterDate = DateTime.UtcNow;
				admin.IsActive = true;

				userManager.Create(admin, config.Password);
				admin.Roles.Add(new IdentityUserRole { RoleId = adminRole.Id, UserId = admin.Id });
				admin.Roles.Add(new IdentityUserRole { RoleId = userRole.Id, UserId = admin.Id });
				context.SaveChanges();

				if (generateDummyUsers)
				{
					this.InitializeDummyUsers(context, userManager);
				}
			}
		}

		private void InitializeDummyUsers(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
		{
			for (int i = 0; i < 10; i++)
			{
				ApplicationUser newUser = new ApplicationUser();
				newUser.UserName = "user" + i.ToString() + "@gmail.com";
				newUser.FirstName = "UserFirstName" + i.ToString();
				newUser.LastName = "UserLastName" + i.ToString();
				newUser.Email = "user" + i.ToString() + "@gmail.com";
				newUser.PhoneNumber = "123456789";
				newUser.RegisterDate = DateTime.UtcNow;
				newUser.IsActive = true;

				userManager.Create(newUser, "1234567");

				userManager.AddToRole(newUser.Id, "User");
			}

			context.SaveChanges();
		}

		private byte[] LoadImageData(string filePath)
		{
			byte[] imageData = File.ReadAllBytes(filePath);

			if (imageData != null && imageData.Length > 0)
			{
				return imageData;
			}
			else
			{
				return null;
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
			if (HttpContext.Current != null)
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
							Slug = fileName,
							DateCreated = DateTime.UtcNow,
							LastModified = DateTime.UtcNow
						};

						context.Pages.Add(newPage);
					}
				}

				context.SaveChanges();
			}
		}

		private bool PageExistsInDatabase(ApplicationDbContext context, string fileName)
		{
			if (context.Pages.Any(p => p.Slug == fileName))
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
