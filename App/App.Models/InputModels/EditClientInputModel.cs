using System.ComponentModel.DataAnnotations;

namespace App.Models.InputModels
{
	public class EditClientInputModel
	{
		[Required(ErrorMessage = " * Задължително!")]
		[Display(Name = "Име:")]
		public string FirstName { get; set; }

		[Required(ErrorMessage = " * Задължително!")]
		[Display(Name = "Фамилия:")]
		public string LastName { get; set; }

		[Display(Name = "Фирма:")]
		public string Company { get; set; }

		[Display(Name = "Град:")]
		public string City { get; set; }

		[Display(Name = "Адрес:")]
		public string Address { get; set; }

		[Display(Name = "Адрес за доставка:")]
		public string DeliveryAddress { get; set; }

		[Required(ErrorMessage = " * Телефона е задължителен!")]
		[Display(Name = "Телефон за връзка:")]
		public string Phone { get; set; }

		[Display(Name = "Данни за фактура:")]
		[DataType(DataType.MultilineText)]
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
