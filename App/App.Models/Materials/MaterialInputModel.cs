using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace App.Models.Materials
{
	public class MaterialInputModel
	{
		[Required(ErrorMessage = " *Задължително" )]
		[Display(Name = "Име: ")]
		[StringLength(250, MinimumLength = 3, ErrorMessage = "Невалидно име - Максимална дължина 250 символа, минимална 3")]
		public string Name { get; set; }

		public int CategoryId { get; set; }

		[Required(ErrorMessage = "Задължително!")]
		[Display(Name = "Категория")]
		public int SelectedCategoryId { get; set; }
		public IEnumerable<SelectListItem> Categories { get; set; }

		public HttpPostedFileBase UploadedImage { get; set; }

		public byte[] CurrentImage { get; set; }
	}
}