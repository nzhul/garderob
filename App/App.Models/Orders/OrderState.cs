namespace App.Models.Orders
{
	public enum OrderState
	{
		New,
		WaitingClientResponse,
		InProduction,
		Done,
		Canceled
	}
}