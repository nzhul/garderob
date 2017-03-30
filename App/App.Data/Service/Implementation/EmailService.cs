using System;
using App.Data.Service.Abstraction;
using App.Data.Service.Messaging;

namespace App.Data.Service.Implementation
{
	public class EmailService : IMessagingService
	{
		public void Notify(string userId, MessageData messageData)
		{
			throw new NotImplementedException();
		}
	}
}
