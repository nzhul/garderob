﻿using App.Data.Repositories;
using App.Models;
using App.Models.Documents;
using App.Models.Images;
using App.Models.Materials;
using App.Models.Orders;
using App.Models.Pages;
using App.Models.Testimonials;
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

		IRepository<MaterialCategory> MaterialCategories { get; }

		IRepository<Testimonial> Testimonials { get; }

		IRepository<Image> Images { get; }

		IRepository<Document> Documents { get; }

		int SaveChanges();
	}
}