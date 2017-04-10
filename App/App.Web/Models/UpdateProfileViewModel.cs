using System.ComponentModel.DataAnnotations;

namespace App.Web.Models
{
	public class UpdateProfileViewModel
	{
		[Required(ErrorMessage = " * Задължително!")]
		[Display(Name = "Име:")]
		[StringLength(100, MinimumLength = 3, ErrorMessage = " Максимална дължина 100 символа, минимална 3")]
		public string FirstName { get; set; }

		[Required(ErrorMessage = " * Задължително!")]
		[Display(Name = "Фамилия:")]
		[StringLength(100, MinimumLength = 3, ErrorMessage = " Максимална дължина 100 символа, минимална 3")]
		public string LastName { get; set; }

		[Display(Name = "Професия:")]
		[StringLength(100, MinimumLength = 3, ErrorMessage = " Максимална дължина 100 символа, минимална 3")]
		public string JobTitle { get; set; }

		[Display(Name = "Фирма:")]
		[StringLength(100, MinimumLength = 3, ErrorMessage = " Максимална дължина 100 символа, минимална 3")]
		public string Company { get; set; }

		[Display(Name = "Град:")]
		[StringLength(100, MinimumLength = 3, ErrorMessage = " Максимална дължина 100 символа, минимална 3")]
		public string City { get; set; }

		[Display(Name = "Адрес:")]
		[StringLength(2500, MinimumLength = 3, ErrorMessage = " Максимална дължина 2500 символа, минимална 3")]
		public string Address { get; set; }

		[Display(Name = "Адрес за доставка:")]
		[StringLength(2500, MinimumLength = 3, ErrorMessage = " Максимална дължина 2500 символа, минимална 3")]
		public string DeliveryAddress { get; set; }

		[Required(ErrorMessage = " * Телефона е задължителен!")]
		[Display(Name = "Телефон за връзка:")]
		[StringLength(250, MinimumLength = 3, ErrorMessage = " Максимална дължина 250 символа, минимална 3")]
		public string Phone { get; set; }

		[Display(Name = "Данни за фактура:")]
		[DataType(DataType.MultilineText)]
		[StringLength(5000, MinimumLength = 3, ErrorMessage = " Максимална дължина 5000 символа, минимална 3")]
		public string InvoiceData { get; set; }

		public string FullName
		{
			get
			{
				return string.Format("{0} {1}", this.FirstName, this.LastName);
			}
		}

		public byte[] ProfileImage { get; set; }
	}
}