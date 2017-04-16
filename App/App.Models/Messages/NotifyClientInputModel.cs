namespace App.Models.Messages
{
	public class NotifyClientInputModel
	{
		public string ClientEmail { get; set; }

		public string ClientFullName { get; set; }

		public string MessageText { get; set; }

		public string OrderName { get; set; }

		public int OrderId { get; set; }
	}
}