using App.Data.Service.Abstraction;
using App.Data.Service.Messaging;
using App.Models;
using System.Configuration;
using System.Net.Mail;

namespace App.Data.Service.Implementation
{
	public class EmailService : IMessagingService
	{
		public void Notify(ApplicationUser user, MessageData messageData)
		{
			this.Notify(user.Email, messageData);
		}

		public void Notify(string userEmail, MessageData messageData)
		{
			string sender = ConfigurationManager.AppSettings["emailSender"];
			string receiver = userEmail;

			MailMessage mailMessage = new MailMessage(sender, receiver);
			mailMessage.IsBodyHtml = true;
			mailMessage.Subject = messageData.MessageTitle;
			mailMessage.Body = messageData.MessageBody;

			SmtpClient smtpClient = new SmtpClient();
			smtpClient.Send(mailMessage);
		}
	}
}