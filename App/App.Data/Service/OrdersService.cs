using App.Models.Orders;
using System;
using System.Linq;

namespace App.Data.Service
{
	public class OrdersService : IOrdersService
	{
		private readonly IUoWData Data;

		public OrdersService(IUoWData data)
		{
			this.Data = data;
		}

		public bool DeleteOrder(int id)
		{
			Order dbOrder = this.Data.Orders.Find(id);

			if (dbOrder == null)
			{
				return false;
			}

			this.Data.Orders.Delete(id);
			this.Data.SaveChanges();
			return true;
		}

		public bool DeleteOrderCategory(int id)
		{
			OrderCategory dbCategory = this.Data.OrderCategories.Find(id);

			if (dbCategory == null)
			{
				return false;
			}

			this.Data.OrderCategories.Delete(id);
			this.Data.SaveChanges();
			return true;
		}

		public Order GetOrderById(int id)
		{
			return this.Data.Orders.Find(id);
		}

		public IQueryable<OrderCategory> GetOrderCategories()
		{
			return this.Data.OrderCategories.All();
		}

		public OrderCategory GetOrderCategoryById(int id)
		{
			return this.Data.OrderCategories.Find(id);
		}

		public IQueryable<Order> GetOrdersByCategory(string category)
		{
			return this.Data.Orders.All().Where(o => o.OrderCategory.Name == category);
		}

		public IQueryable<Order> GetOrdersByUserId(string userId)
		{
			return this.Data.Orders.All().Where(o => o.Client.Id == userId);
		}

		public bool UpdateOrder(int id, OrderInputModel inputModel)
		{
			throw new NotImplementedException();
		}

		public bool UpdateOrderCategory(int id, OrderCategoryInputModel inputModel)
		{
			throw new NotImplementedException();
		}
	}
}
