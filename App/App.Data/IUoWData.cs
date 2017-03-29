using App.Data.Repositories;
using App.Models;
using App.Models.Materials;
using App.Models.Orders;
using App.Models.Pages;
using System.Data.Entity;

namespace App.Data
{
	public interface IUoWData
	{
		DbContext Context { get; }

		IRepository<ApplicationUser> Users { get; }

		IRepository<Page> Pages { get; }

		IRepository<Order> Orders { get; }

		IRepository<OrderCategory> OrderCategories { get; }

		IRepository<Material> Materials { get; }

		IRepository<SurfaceMaterial> SurfaceMaterials { get; }

		IRepository<MaterialCategory> MaterialCategories { get; }

		int SaveChanges();
	}
}