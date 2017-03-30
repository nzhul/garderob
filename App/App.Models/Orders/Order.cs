using App.Models.Images;
using App.Models.Materials;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Models.Orders
{
	public class Order
	{
		private ICollection<Image> sketchImages;
		private ICollection<Image> designImages;
		private ICollection<Image> resultImages;

		public Order()
		{
			this.sketchImages = new HashSet<Image>();
			this.designImages = new HashSet<Image>();
			this.resultImages = new HashSet<Image>();
		}

		[Key]
		public int Id { get; set; }

		public string Title { get; set; }

		public string OrderText { get; set; }

		public DateTime RequestDate { get; set; }

		public DateTime OfferDate { get; set; }

		public DateTime CompleteDate { get; set; }

		public OrderState State { get; set; }

		public virtual ICollection<Image> SketchImages
		{
			get { return this.sketchImages; }
			set { this.sketchImages = value; }
		}

		public virtual ICollection<Image> DesignImages
		{
			get { return this.designImages; }
			set { this.designImages = value; }
		}

		public virtual ICollection<Image> ResultImages
		{
			get { return this.resultImages; }
			set { this.resultImages = value; }
		}

		[ForeignKey("Client")]
		public string ClientId { get; set; }

		public virtual ApplicationUser Client { get; set; }

		[ForeignKey("OrderCategory")]
		public int OrderCategoryId { get; set; }

		public virtual OrderCategory OrderCategory { get; set; }

		public Material BaseMaterial { get; set; }

		public Material DoorsMaterial { get; set; }

		public Material FazerMaterial { get; set; }

		public Material HandlesMaterial { get; set; }
	}
}
