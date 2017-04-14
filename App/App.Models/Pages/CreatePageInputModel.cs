using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace App.Models.Pages
{
	public class CreatePageInputModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Името на страницата е задължително:")]
        [StringLength(250, MinimumLength = 3, ErrorMessage = "Невалидно име - Максимална дължина 250 символа, минимална 3")]
        [Display(Name = "Име:")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Описанието е задължително!")]
        [AllowHtml]
        [Display(Name = "Описание:")]
        public string Summary { get; set; }

        [Required(ErrorMessage = "Съдържанието е задължително!")]
        [AllowHtml]
        [Display(Name = "Съдържание:")]
		[DataType("codemirror")]
		[UIHint("codemirror")]
		//[DataType(DataType.MultilineText)]
		public string Content { get; set; }

        [Display(Name = "Позиция:")]
        public int DisplayOrder { get; set; }

		[Display(Name = "Адрес на страницата (Slug):")]
		public string Slug { get; set; }
	}
}
