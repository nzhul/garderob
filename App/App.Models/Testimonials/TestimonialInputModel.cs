using System.ComponentModel.DataAnnotations;

namespace App.Models.Testimonials
{
	public class TestimonialInputModel
	{
		[Required(ErrorMessage = " * Задължително!")]
		public int OrderId { get; set; }

		[Range(0, 5)]
		public double Rating { get; set; }

		[Required(ErrorMessage = " * Задължително!")]
		[Display(Name = "Съдържание на атестата:")]
		[DataType(DataType.MultilineText)]
		[StringLength(2500, MinimumLength = 3, ErrorMessage = " *Невалидна дължина!")]
		public string Text { get; set; }

		public string OrderTitle { get; set; }
	}
}