using App.Models.Images;
using App.Models.Materials;
using App.Models.Testimonials;
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
		private ICollection<Testimonial> testimonials;

		public Order()
		{
			this.sketchImages = new HashSet<Image>();
			this.designImages = new HashSet<Image>();
			this.resultImages = new HashSet<Image>();
			this.testimonials = new HashSet<Testimonial>();
		}

		[Key]
		public int Id { get; set; }

		public string Title { get; set; }

		public string Slug { get; set; }

		public string OrderText { get; set; }

		public DateTime RequestDate { get; set; }

		public DateTime OfferDate { get; set; }

		public DateTime CompleteDate { get; set; }

		public DateTime LastModified { get; set; }

		public OrderState State { get; set; }

		public decimal Price { get; set; }

		public int Count { get; set; }

		public bool Installation { get; set; }

		public bool IsInCart { get; set; }

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

		public virtual ICollection<Testimonial> Testimonials
		{
			get { return this.testimonials; }
			set { this.testimonials = value; }
		}

		[ForeignKey("Client")]
		public string ClientId { get; set; }

		public virtual ApplicationUser Client { get; set; }

		[ForeignKey("OrderCategory")]
		public int OrderCategoryId { get; set; }

		public virtual OrderCategory OrderCategory { get; set; }


		//?? ForeignKeys do not work here for some reason

		public int? BaseMaterialId { get; set; }
		public virtual Material BaseMaterial { get; set; }

		public int? DoorsMaterialId { get; set; }
		public virtual Material DoorsMaterial { get; set; }

		public int? FazerMaterialId { get; set; }
		public virtual Material FazerMaterial { get; set; }

		public int? HandlesMaterialId { get; set; }
		public virtual Material HandlesMaterial { get; set; }
	}
}