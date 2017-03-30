﻿using App.Data.Service.Abstraction;
using App.Data.Service.Messaging;
using App.Models.Images;
using App.Models.Orders;
using AutoMapper;
using System;
using System.Linq;

namespace App.Data.Service.Implementation
{
	public class OrdersService : IOrdersService
	{
		private readonly IUoWData Data;
		private readonly IMessagingService MessagingService;

		public OrdersService(IUoWData data, IMessagingService messagingService)
		{
			this.Data = data;
			this.MessagingService = messagingService;
		}

		public void AddDesignImage(int orderId, Image image, bool notifyClient, bool notifyAdmin)
		{
			Order dbOrder = this.GetOrder(orderId);

			if (dbOrder != null)
			{
				dbOrder.DesignImages.Add(image);
				this.Data.SaveChanges();
			}

			if (notifyClient)
			{
				MessageData message = new MessageData();
				this.MessagingService.Notify(dbOrder.ClientId, message);
			}

			if (notifyAdmin)
			{
				MessageData message = new MessageData();
				this.MessagingService.Notify("admin-id", message); //TODO: get the admin from configuration (Email)
			}
		}

		public void AddResultImage(int orderId, Image image, bool notifyClient, bool notifyAdmin)
		{
			Order dbOrder = this.GetOrder(orderId);

			if (dbOrder != null)
			{
				dbOrder.ResultImages.Add(image);
				this.Data.SaveChanges();
			}

			if (notifyClient)
			{
				MessageData message = new MessageData();
				this.MessagingService.Notify(dbOrder.ClientId, message);
			}

			if (notifyAdmin)
			{
				MessageData message = new MessageData();
				this.MessagingService.Notify("admin-id", message); //TODO: get the admin from configuration (Email)
			}
		}

		public void AddSketchImage(int orderId, Image image, bool notifyClient, bool notifyAdmin)
		{
			Order dbOrder = this.GetOrder(orderId);

			if (dbOrder != null)
			{
				dbOrder.SketchImages.Add(image);
				this.Data.SaveChanges();
			}

			if (notifyClient)
			{
				MessageData message = new MessageData();
				this.MessagingService.Notify(dbOrder.ClientId, message);
			}

			if (notifyAdmin)
			{
				MessageData message = new MessageData();
				this.MessagingService.Notify("admin-id", message); //TODO: get the admin from configuration (Email)
			}
		}

		public void ChangeOrderState(int orderId, OrderState newState, bool notifyClient, bool notifyAdmin)
		{
			Order dbOrder = this.GetOrder(orderId);

			if (dbOrder != null)
			{
				dbOrder.State = newState;
				this.Data.SaveChanges();
			}

			if (notifyClient)
			{
				MessageData message = new MessageData();
				this.MessagingService.Notify(dbOrder.ClientId, message);
			}

			if (notifyAdmin)
			{
				MessageData message = new MessageData();
				this.MessagingService.Notify("admin-id", message); //TODO: get the admin from configuration (Email)
			}
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

		public Order GetOrder(int id)
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

		public IQueryable<Order> GetUserOrders(string userId)
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
			Order dbOrder = this.GetOrder(id);

			if (dbOrder != null)
			{
				// TODO: DO the mapping with Automapper and manual for images
				this.Data.SaveChanges();
				return true;
			}

			return false;
		}

		public bool UpdateOrderCategory(int id, OrderCategoryInputModel inputModel)
		{
			OrderCategory dbCategory = this.Data.OrderCategories.Find(id);
			if (dbCategory != null)
			{
				//TODO: Automapper
				this.Data.SaveChanges();
				return true;
			}

			return false;
		}
	}
}