namespace App.Data.Migrations
{
	using App.Models;
	using App.Models.Pages;
	using System;
	using System.Collections.Generic;
	using System.Data.Entity;
	using System.Data.Entity.Migrations;
	using System.IO;
	using System.Linq;
	using System.Web;

	public sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
	{
		static Random rand = new Random();
		public Configuration()
		{
			AutomaticMigrationsEnabled = true;
			AutomaticMigrationDataLossAllowed = true;
		}

		protected override void Seed(ApplicationDbContext context)
		{
			//this.AddInitialItemCategories(context);
			//this.AddInitialItems(context);
			this.AddInitialStaticPages(context);
		}

		private void AddInitialStaticPages(ApplicationDbContext context)
		{
			if (!context.Pages.Any())
			{
				string folderPath = HttpContext.Current.Server.MapPath("~/App_Data/StaticPages");
				IEnumerable<string> pages = Directory.EnumerateFiles(folderPath);

				foreach (var pagePath in pages)
				{
					string fileName = Path.GetFileNameWithoutExtension(pagePath);
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

				context.SaveChanges();
			}
		}

		private void AddInitialItemCategories(ApplicationDbContext context)
		{
			if (!context.ItemCategories.Any())
			{
				for (int i = 0; i < 4; i++)
				{
					var newItemCategory = new ItemCategory
					{
						Name = "Item category " + i,
						DateAdded = DateTime.Now
					};
					context.ItemCategories.Add(newItemCategory);
				}
				context.SaveChanges();
			}
		}

		private void AddInitialItems(ApplicationDbContext context)
		{
			if (!context.Items.Any())
			{
				for (int i = 0; i < 10; i++)
				{
					var newItem = new Item
					{
						Name = "Item " + i,
						Price = rand.Next(30, 80),
						Summary = "Short Description of the item " + i,
						Description = "Long Description of the item " + i,
						DateAdded = DateTime.Now,
						ItemCategoryId = rand.Next(1, 5)
					};
					context.Items.Add(newItem);
				}
				context.SaveChanges();
			}
		}
	}
}
