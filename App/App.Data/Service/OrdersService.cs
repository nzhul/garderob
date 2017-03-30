using App.Models.Orders;
using AutoMapper;
using System;
using System.Linq;
using App.Models.Images;

namespace App.Data.Service
{
	public class OrdersService : IOrdersService
	{
		private readonly IUoWData Data;

		public OrdersService(IUoWData data)
		{
			this.Data = data;
		}

		public void AddDesignImage(int orderId, Image image, bool notifyClient, bool notifyAdmin)
		{
			throw new NotImplementedException();
		}

		public void AddResultImage(int orderId, Image image, bool notifyClient, bool notifyAdmin)
		{
			throw new NotImplementedException();
		}

		public void AddSketchImage(int orderId, Image image, bool notifyClient, bool notifyAdmin)
		{
			throw new NotImplementedException();
		}

		public void ChangeOrderState(int orderId, OrderState newState, bool notifyClient, bool notifyAdmin)
		{
			throw new NotImplementedException();
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

		public int MakeOrder(OrderInputModel model)
		{
			// Check what happens with ClientId and OrderId. They are required
			// If the client id is not automaticaly mapped - use the provided cliendId from the model. Find the user in the database and assign it ot "Client" property
			// It must be set to default "Other" category, and later the admin can change the category from the backend

			Order newOrder = new Order();
			newOrder = Mapper.Map(model, newOrder);
			newOrder.RequestDate = DateTime.UtcNow;

			this.Data.Orders.Add(newOrder);
			this.Data.SaveChanges();

			return newOrder.Id;
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
