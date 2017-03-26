using App.Data.Repositories;
using App.Models;
using App.Models.Materials;
using App.Models.Orders;
using App.Models.Pages;

namespace App.Data
{
	public interface IUoWData
	{
		IRepository<ApplicationUser> Users { get; }

		IRepository<Page> Pages { get; }

		IRepository<Order> Orders { get; }

		IRepository<OrderCategory> OrderCategories { get; }

		IRepository<Material> Materials { get; }

		IRepository<MaterialCategory> MaterialCategories { get; }

		int SaveChanges();
	}
}