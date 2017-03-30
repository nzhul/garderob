namespace App.Models.Orders
{
	public enum OrderState
	{
		New,
		WaitingClientResponse,
		InProduction,
		Done,
		Rejected,
		Canceled
	}
}