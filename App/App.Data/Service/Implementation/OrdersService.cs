using App.Data.Service.Abstraction;
using App.Data.Service.Messaging;
using App.Data.Utilities;
using App.Models;
using App.Models.Images;
using App.Models.InputModels;
using App.Models.Orders;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Data.Service.Implementation
{
	public class OrdersService : IOrdersService
	{
		private readonly IUoWData Data;
		private readonly IMessagingService MessagingService;
		private const string defaultBigImageQuery = "width=1920&height=1080&crop=auto&scale=both&format=jpg";
		private const string defaultSmallImageQuery = "width=210&height=203&crop=auto&format=jpg";

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

		public bool ConfirmOrderOffer(int orderId, int count, bool notifyAdmin)
		{
			Order dbOrder = this.Data.Orders.Find(orderId);

			if (dbOrder == null)
			{
				return false;
			}

			dbOrder.State = OrderState.OfferConfirmed;
			dbOrder.Count = count;
			this.Data.SaveChanges();

			if (notifyAdmin)
			{
				MessageData message = new MessageData();
				this.MessagingService.Notify("admin-id", message); //TODO: get the admin from configuration (Email)
			}

			return true;

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

		public OrderCategory GetOrderCategoryBySlug(string slug)
		{
			return this.Data.OrderCategories.All().Where(c => c.Slug == slug).FirstOrDefault();
		}

		public IQueryable<Order> GetOrdersByCategory(string category)
		{
			return this.Data.Orders.All().Where(o => o.OrderCategory.Name == category);
		}

		public IQueryable<Order> GetUserOrders(string userId)
		{
			return this.Data.Orders.All().Where(o => o.Client.Id == userId).OrderByDescending(o => o.LastModified);
		}

		public IQueryable<Order> GetUserCart(string userId)
		{
			ApplicationUser dbUser = this.Data.Users.Find(userId);

			if (dbUser != null)
			{
				return dbUser.Cart.AsQueryable();
			}
			else
			{
				throw new ArgumentNullException("Cannot find cart for user with ID: " + userId);
			}
		}

		public int MakeOrder(OrderInputModel model)
		{
			Order newOrder = new Order();
			newOrder = Mapper.Map(model, newOrder);
			newOrder.RequestDate = DateTime.UtcNow;
			newOrder.OfferDate = DateTime.MaxValue;
			newOrder.CompleteDate = DateTime.MaxValue;
			newOrder.LastModified = DateTime.UtcNow;
			newOrder.Count = 1;

			this.Data.Orders.Add(newOrder);
			this.Data.SaveChanges();

			foreach (HttpPostedFileBase image in model.PostedSketches)
			{
				byte[] bigImageData = ImageUtilities.CropImage(image, OrdersService.defaultBigImageQuery);
				byte[] smallImageData = ImageUtilities.CropImage(image, OrdersService.defaultSmallImageQuery); //TODO: Check if this crop is ok

				Image newSketch = new Image
				{
					Big = bigImageData,
					Small = smallImageData
				};

				newOrder.SketchImages.Add(newSketch);
			}

			this.Data.SaveChanges();

			return newOrder.Id;
		}

		//TODO: delete this
		//public bool UpdateOrder(int id, OrderInputModel inputModel)
		//{
		//	Order dbOrder = this.GetOrder(id);

		//	if (dbOrder != null)
		//	{
		//		// TODO: DO the mapping with Automapper and manual for images
		//		this.Data.SaveChanges();
		//		return true;
		//	}

		//	return false;
		//}

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

		public Order AddCartItem(int orderId, int orderCount, bool installation, string userId)
		{
			Order dbOrder = this.Data.Orders.Find(orderId);
			ApplicationUser dbUser = this.Data.Users.Find(userId);

			if (dbOrder != null && dbUser != null && dbOrder.Client.Id == dbUser.Id)
			{
				dbOrder.IsInCart = true;
				dbOrder.Count = orderCount;
				dbOrder.Installation = installation;
				dbUser.Cart.Add(dbOrder);
				this.Data.SaveChanges();
				return dbOrder;
			}
			else
			{
				return null;
			}
		}

		public bool RemoveCartItem(int orderId, string userId)
		{
			Order dbOrder = this.Data.Orders.Find(orderId);
			ApplicationUser dbUser = this.Data.Users.Find(userId);

			if (dbOrder != null && dbUser != null && dbOrder.Client.Id == dbUser.Id)
			{
				dbOrder.IsInCart = false;
				dbUser.Cart.Remove(dbOrder);
				this.Data.SaveChanges();
				return true;
			}
			else
			{
				return false;
			}
		}

		public bool OrderNow(string userId)
		{
			// get all items from applicationuser.cart
			// remove them from cart collection
			// set their status to InProduction
			// Send notifications to both client and administrator

			ApplicationUser currentUser = this.Data.Users.Find(userId);

			if (currentUser != null)
			{
				ICollection<Order> cartItems = currentUser.Cart;
				//this.SendOrderSuccessMessage(userId, cartItems);
				//this.SendAdminNotification("adminId", cartItems);

				foreach (Order order in cartItems)
				{
					order.IsInCart = false;
					order.State = OrderState.InProduction;
					this.Data.SaveChanges();
				}

				currentUser.Cart.ToList().ForEach(x => currentUser.Cart.Remove(x));
				this.Data.SaveChanges();

				return true;
			}

			return false;
		}

		private void SendAdminNotification(string adminId, ICollection<Order> cartItems)
		{
			MessageData message = new MessageData();

			foreach (Order order in cartItems)
			{

			}

			this.MessagingService.Notify(adminId, message);
		}

		private void SendOrderSuccessMessage(string userId, ICollection<Order> cartItems)
		{
			MessageData message = new MessageData();

			foreach (Order order in cartItems)
			{

			}

			this.MessagingService.Notify(userId, message);
		}

		public IQueryable<Order> GetAllDoneOrders()
		{
			return this.Data.Orders.All().Where(o => o.State == OrderState.Done);
		}

		public IQueryable<Order> GetOrdersByState(OrderState state)
		{
			return this.Data.Orders.All().Where(o => o.State == state);
		}

		public bool UpdateOrder(int id, EditOrderInputModel model)
		{
			Order dbOrder = this.GetOrder(id);
			if (dbOrder != null)
			{
				dbOrder = Mapper.Map(model, dbOrder);

				if (model.PostedSketches != null && model.PostedSketches.Count > 0 && model.PostedSketches[0] != null)
				{
					foreach (HttpPostedFileBase image in model.PostedSketches)
					{
						byte[] smallImageData = ImageUtilities.CropImage(image, OrdersService.defaultSmallImageQuery);
						byte[] bigImageData = ImageUtilities.CropImage(image, OrdersService.defaultBigImageQuery);

						Image newImage = new Image
						{
							Small = smallImageData,
							Big = bigImageData
						};

						dbOrder.SketchImages.Add(newImage);
					}
				}

				if (model.PostedDesigns != null && model.PostedDesigns.Count > 0 && model.PostedDesigns[0] != null)
				{
					foreach (HttpPostedFileBase image in model.PostedDesigns)
					{
						byte[] smallImageData = ImageUtilities.CropImage(image, OrdersService.defaultSmallImageQuery);
						byte[] bigImageData = ImageUtilities.CropImage(image, OrdersService.defaultBigImageQuery);

						Image newImage = new Image
						{
							Small = smallImageData,
							Big = bigImageData
						};

						dbOrder.DesignImages.Add(newImage);
					}
				}

				if (model.PostedResults != null && model.PostedResults.Count > 0 && model.PostedResults[0] != null)
				{
					foreach (HttpPostedFileBase image in model.PostedResults)
					{
						byte[] smallImageData = ImageUtilities.CropImage(image, OrdersService.defaultSmallImageQuery);
						byte[] bigImageData = ImageUtilities.CropImage(image, OrdersService.defaultBigImageQuery);

						Image newImage = new Image
						{
							Small = smallImageData,
							Big = bigImageData
						};

						dbOrder.ResultImages.Add(newImage);
					}
				}

				if (model.State == OrderState.Done)
				{
					dbOrder.CompleteDate = DateTime.UtcNow;
				}

				if (model.State == OrderState.WaitingClientResponse)
				{
					dbOrder.OfferDate = DateTime.UtcNow;
				}


				dbOrder.LastModified = DateTime.UtcNow;
				this.Data.SaveChanges();

				return true;
			}
			else
			{
				return false;
			}
		}
	}
}