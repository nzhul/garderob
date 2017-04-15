using App.Models.Documents;
using App.Models.Images;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace App.Models.Materials
{
	public class EditMaterialCategoryInputModel
	{
		public int Id { get; set; }

		[Required(ErrorMessage = " * Задължително:")]
		[StringLength(250, MinimumLength = 3, ErrorMessage = "Невалидно име - Максимална дължина 250 символа, минимална 3")]
		[Display(Name = "Име:")]
		public string Name { get; set; }

		[Required(ErrorMessage = " * Задължително:")]
		[StringLength(250, MinimumLength = 3, ErrorMessage = "Невалидно име - Максимална дължина 250 символа, минимална 3")]
		public string Slug { get; set; }

		[Required(ErrorMessage = " * Задължително!")]
		[AllowHtml]
		[Display(Name = "Описание:")]
		[DataType(DataType.MultilineText)]
		public string Description { get; set; }

		[Required( ErrorMessage = " * Задължително")]
		[Display(Name = "Размер Малка картинка:")]
		public string SmallImageSize { get; set; }

		[Required(ErrorMessage = " * Задължително")]
		[Display(Name = "Размер Средна картинка:")]
		public string MediumImageSize { get; set; }

		[Required(ErrorMessage = " * Задължително")]
		[Display(Name = "Размер Голяма картинка:")]
		public string BigImageSize { get; set; }

		public Image Image { get; set; }

		public Document Pdf { get; set; }

		public HttpPostedFileBase PostedImage { get; set; }

		public HttpPostedFileBase PostedPdf { get; set; }
	}
}
