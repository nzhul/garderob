using App.Models.Images;
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

		public MaterialCategory Category { get; set; }

		public virtual Image Image { get; set; }

	}
}