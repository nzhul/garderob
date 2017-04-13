using System;
using System.ComponentModel.DataAnnotations;

namespace App.Models.InputModels
{
	public class EditTestimonialInputModel
	{
		[Required(ErrorMessage = " * Задължително!")]
		[Range(0, 5)]
		[Display(Name = "Рейтинг:")]
		public double Rating { get; set; }

		[Required(ErrorMessage = " * Задължително!")]
		[Display(Name = "Съдържание:")]
		[DataType(DataType.MultilineText)]
		public string Text { get; set; }

		[Display(Name = "Одобрен:")]
		public bool IsApproved { get; set; }

		public DateTime SubmissionDate { get; set; }

		public string OrderTitle { get; set; }

		public int OrderId { get; set; }

		public string ClientId { get; set; }

		public string ClientFullName { get; set; }
	}
}