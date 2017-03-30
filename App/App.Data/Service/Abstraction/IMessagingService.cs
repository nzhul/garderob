using App.Data.Service.Messaging;

namespace App.Data.Service.Abstraction
{
	public interface IMessagingService
	{
		void Notify(string userId, MessageData messageData);
	}
}
