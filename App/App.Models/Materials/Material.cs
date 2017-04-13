using App.Models.Images;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Models.Materials
{
	public class Material
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Slug { get; set; }

		[ForeignKey("Category")]
		public int CategoryId { get; set; }

		public virtual MaterialCategory Category { get; set; }

		public double Price { get; set; }

		public virtual Image Image { get; set; }

		public DateTime DateCreated { get; set; }

		public DateTime LastModified { get; set; }
	}
}