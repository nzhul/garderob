using App.Models.Documents;
using App.Models.Images;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

		public string Slug { get; set; }

		public int Order { get; set; }

		public string SmallImageSize { get; set; }

		public string MediumImageSize { get; set; }

		public string BigImageSize { get; set; }

		public virtual Document Pdf { get; set; }

		public virtual Image Image { get; set; }

		public DateTime DateCreated { get; set; }

		public DateTime LastModified { get; set; }

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
