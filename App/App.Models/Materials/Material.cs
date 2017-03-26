using App.Models.Images;
using System.Collections.Generic;

namespace App.Models.Materials
{
	public class Material
	{
		private ICollection<Image> images;

		public Material()
		{
			this.images = new HashSet<Image>();
		}

		public int Id { get; set; }

		public string Name { get; set; }

		public int CategoryId { get; set; }

		public MaterialCategory Category { get; set; }

		public virtual ICollection<Image> Images
		{
			get
			{
				return this.images;
			}
			set
			{
				this.images = value;
			}
		}

	}
}
