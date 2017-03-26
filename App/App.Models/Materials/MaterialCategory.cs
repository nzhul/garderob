using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace App.Models.Materials
{
	public class MaterialCategory
	{
		private ICollection<Material> materials;

		public MaterialCategory()
		{
			this.materials = new HashSet<Material>();
		}

		[Key]
		public int Id { get; set; }

		public string Name { get; set; }

		public string Description { get; set; }

		public virtual ICollection<Material> Materials
		{
			get
			{
				return this.materials;
			}

			set
			{
				this.materials = value;
			}
		}
	}
}
