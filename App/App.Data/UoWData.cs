using App.Data.Repositories;
using App.Models;
using App.Models.Materials;
using App.Models.Orders;
using App.Models.Pages;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace App.Data
{
	public class UoWData : IUoWData
	{
		private DbContext context;
		private IDictionary<Type, object> repositories;

		public UoWData()
			: this(new ApplicationDbContext())
		{
		}

		public UoWData(DbContext context)
		{
			this.Context = context;
			this.repositories = new Dictionary<Type, object>();
		}

		public DbContext Context
		{
			get
			{
				return this.context;
			} 
			private set
			{
				this.context = value;
			}
		}

		public IRepository<ApplicationUser> Users
		{
			get { return this.GetRepository<ApplicationUser>(); }
		}

		public IRepository<Page> Pages
		{
			get { return this.GetRepository<Page>(); }
		}

		public IRepository<Order> Orders
		{
			get
			{
				return this.GetRepository<Order>();
			}
		}

		public IRepository<OrderCategory> OrderCategories
		{
			get
			{
				return this.GetRepository<OrderCategory>();
			}
		}

		public IRepository<Material> Materials
		{
			get
			{
				return this.GetRepository<Material>();
			}
		}

		public IRepository<MaterialCategory> MaterialCategories
		{
			get
			{
				return this.GetRepository<MaterialCategory>();
			}
		}

		public int SaveChanges()
		{
			return this.context.SaveChanges();
		}

		private IRepository<T> GetRepository<T>() where T : class
		{
			var typeOfRepository = typeof(T);
			if (!this.repositories.ContainsKey(typeOfRepository))
			{
				var newRepository = Activator.CreateInstance(typeof(EFRepository<T>), context);
				this.repositories.Add(typeOfRepository, newRepository);
			}

			return (IRepository<T>)this.repositories[typeOfRepository];
		}
	}
}
