using App.Data.Migrations;
using App.Models;
using App.Models.Materials;
using App.Models.Orders;
using App.Models.Pages;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace App.Data
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public ApplicationDbContext()
			: base("DefaultConnection", throwIfV1Schema: false)
		{
			Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
		}

		public static ApplicationDbContext Create()
		{
			return new ApplicationDbContext();
		}

		public IDbSet<Page> Pages { get; set; }

		public IDbSet<Order> Orders { get; set; }

		public IDbSet<OrderCategory> OrderCategories { get; set; }

		public IDbSet<Material> Materials { get; set; }

		public IDbSet<MaterialCategory> MaterialCategories { get; set; }
	}
}