using App.Models.Images;

namespace App.Models.Materials
{
	public class Material
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public int CategoryId { get; set; }

		public MaterialCategory Category { get; set; }

		public virtual Image Image { get; set; }

	}
}