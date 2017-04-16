using System;
using App.Data.Service.Abstraction;
using App.Data.Service.Messaging;
using App.Models;

namespace App.Data.Service.Implementation
{
	public class EmailService : IMessagingService
	{
		public void Notify(ApplicationUser user, MessageData messageData)
		{
			//TODO send email message
		}
	}
}
