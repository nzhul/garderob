using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using Utilities;
using System.Linq;

namespace App.Models.ValidationAttributes
{
	[AttributeUsage(AttributeTargets.Property)]
	public class RequireImageFileAttribute : ValidationAttribute
	{
		private const string DefaultMissingFileErrorMessage = "Please upload a file!";
		private const string DefaultInvalidFileErrorMessage = "Invalid file!";
        private const string DefaultInvalidFileSizeErrorMessage = "Your Image is too large!";
        private const int maxContentLengthSingleFile = 1024 * 1024 * 5; //5 MB
        private const int maxContentLengthTotal = 1024 * 1024 * 20; //20 MB

        public string MissingFileErrorMessage { get; set; }

		public string InvalidFileErrorMessage { get; set; }

        public string InvalidFileSizeErrorMessage { get; set; }

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			IList<HttpPostedFileBase> files = value as IList<HttpPostedFileBase>;

			if (files == null)
			{
				return new ValidationResult(MissingFileErrorMessage ?? DefaultMissingFileErrorMessage);
			}

            int totalFileSize = files.Sum(f => f.ContentLength);

            if (totalFileSize > maxContentLengthTotal)
            {
                return new ValidationResult(InvalidFileSizeErrorMessage ?? DefaultInvalidFileSizeErrorMessage);
            }

            foreach (var file in files)
			{
				if (file == null)
				{
					return new ValidationResult(MissingFileErrorMessage ?? DefaultMissingFileErrorMessage);
				}

                if (file.ContentLength > maxContentLengthSingleFile)
                {
                    return new ValidationResult(InvalidFileSizeErrorMessage ?? DefaultInvalidFileSizeErrorMessage);
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