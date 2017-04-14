using App.Models.Images;
using App.Models.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace App.Models.Materials
{
	public class EditMaterialInputModel
	{
		[Required(ErrorMessage = " *Задължително" )]
		[Display(Name = "Име: ")]
		[StringLength(250, MinimumLength = 3, ErrorMessage = "Невалидно име - Максимална дължина 250 символа, минимална 3")]
		public string Name { get; set; }

		[Required(ErrorMessage = " *Задължително")]
		[StringLength(250, MinimumLength = 3, ErrorMessage = "Невалидно име - Максимална дължина 250 символа, минимална 3")]
		public string Slug { get; set; }

		[Required(ErrorMessage = " * Задължително!")]
		[Display(Name = "Цена: ")]
		public double Price { get; set; }

		[Required(ErrorMessage = "Задължително!")]
		[Display(Name = "Категория:")]
		public int SelectedCategoryId { get; set; }
		public IEnumerable<SelectListItem> Categories { get; set; }

		public HttpPostedFileBase PostedMaterialImage { get; set; }

		public virtual Image Image { get; set; }

		public DateTime DateCreated { get; set; }

		public DateTime LastModified { get; set; }

		[Required(ErrorMessage = " * Задължително")]
		[Display(Name = "Размер Малка картинка:")]
		public string SmallImageSize { get; set; }

		[Required(ErrorMessage = " * Задължително")]
		[Display(Name = "Размер Средна картинка:")]
		public string MediumImageSize { get; set; }

		[Required(ErrorMessage = " * Задължително")]
		[Display(Name = "Размер Голяма картинка:")]
		public string BigImageSize { get; set; }
	}
}