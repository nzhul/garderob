using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using Utilities;

namespace App.Models.ValidationAttributes
{
	[AttributeUsage(AttributeTargets.Property)]
	public class RequireImageFileAttribute : ValidationAttribute
	{
		private const string DefaultMissingFileErrorMessage = "Please upload a file!";
		private const string DefaultInvalidFileErrorMessage = "Invalid file!";

		public string MissingFileErrorMessage { get; set; }

		public string InvalidFileErrorMessage { get; set; }

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			IList<HttpPostedFileBase> files = value as IList<HttpPostedFileBase>;

			if (files == null)
			{
				return new ValidationResult(MissingFileErrorMessage ?? DefaultMissingFileErrorMessage);
			}

			foreach (var file in files)
			{
				if (file == null)
				{
					return new ValidationResult(MissingFileErrorMessage ?? DefaultMissingFileErrorMessage);
				}

				if (!this.IsFileValid(file))
				{
					return new ValidationResult(InvalidFileErrorMessage ?? DefaultInvalidFileErrorMessage);
				}
			}

			return ValidationResult.Success;
		}

		private bool IsFileValid(HttpPostedFileBase file)
		{
			if (file.IsImage())
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}