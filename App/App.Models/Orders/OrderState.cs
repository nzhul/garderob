namespace App.Models.Orders
{
	public enum OrderState
	{
		New,
		WaitingClientResponse,
		OfferConfirmed,
		InProduction,
		Done,
		Rejected,
		Canceled
	}
}