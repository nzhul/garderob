using App.Data.Service.Messaging;
using App.Models;

namespace App.Data.Service.Abstraction
{
	public interface IMessagingService
	{
		void Notify(ApplicationUser user, MessageData messageData);
	}
}
