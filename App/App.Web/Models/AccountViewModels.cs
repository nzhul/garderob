using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace App.Web.Models
{
	public class ExternalLoginConfirmationViewModel
	{
		[Required]
		[Display(Name = "Email")]
		public string Email { get; set; }
	}

	public class ExternalLoginListViewModel
	{
		public string ReturnUrl { get; set; }
	}

	public class SendCodeViewModel
	{
		public string SelectedProvider { get; set; }
		public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
		public string ReturnUrl { get; set; }
		public bool RememberMe { get; set; }
	}

	public class VerifyCodeViewModel
	{
		[Required]
		public string Provider { get; set; }

		[Required]
		[Display(Name = "Code")]
		public string Code { get; set; }
		public string ReturnUrl { get; set; }

		[Display(Name = "Remember this browser?")]
		public bool RememberBrowser { get; set; }

		public bool RememberMe { get; set; }
	}

	public class ForgotViewModel
	{
		[Required]
		[Display(Name = "Email")]
		public string Email { get; set; }
	}

	public class LoginViewModel
	{
		[Required]
		[Display(Name = "Email")]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		public string Password { get; set; }

		[Display(Name = "Remember me?")]
		public bool RememberMe { get; set; }
	}

	public class RegisterViewModel
	{
		[Required(ErrorMessage = " * Задължително!")]
		[EmailAddress(ErrorMessage = " * Невалиден email!")]
		[Display(Name = "Email:")]
		public string Email { get; set; }

		[Required(ErrorMessage = " * Задължително!")]
		[Display(Name = "Име:")]
		public string FirstName { get; set; }

		[Required(ErrorMessage = " * Задължително!")]
		[Display(Name = "Фамилия:")]
		public string LastName { get; set; }

		[Display(Name = "Професия:")]
		public string JobTitle { get; set; }

		[Required(ErrorMessage = " * Паролата е задължителна!")]
		[StringLength(100, ErrorMessage = " * Паролата трябва да е поне дълга минимум {2} символа!", MinimumLength = 6)]
		[DataType(DataType.Password)]
		[Display(Name = "Парола:")]
		public string Password { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = "Повтори паролата:")]
		[Compare("Password", ErrorMessage = " * Паролите не съвпадат!")]
		public string ConfirmPassword { get; set; }

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
		[DataType(DataType.PhoneNumber)]
		public string Phone { get; set; }

		[Display(Name = "Данни за фактура:")]
		[DataType(DataType.MultilineText)]
		public string InvoiceData { get; set; }

		[Range(typeof(bool), "true", "true", ErrorMessage = " Задължително!")]
		[Display(Name = "Запознах се и съм съгласен с общите условия")]
		public bool TermsAccepted { get; set; }
	}

	public class ResetPasswordViewModel
	{
		[Required]
		[EmailAddress]
		[Display(Name = "Email")]
		public string Email { get; set; }

		[Required]
		[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
		[DataType(DataType.Password)]
		[Display(Name = "Password")]
		public string Password { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[Display(Name = "Confirm password")]
		[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
		public string ConfirmPassword { get; set; }

		public string Code { get; set; }
	}

	public class ForgotPasswordViewModel
	{
		[Required]
		[EmailAddress]
		[Display(Name = "Email")]
		public string Email { get; set; }
	}
}
