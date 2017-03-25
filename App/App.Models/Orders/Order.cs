using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace App.Models.Orders
{
	public class Order
	{
		// if this do not work - extract image properties into OrderImage class
		// order image class will have property ImageData of type byte[]
		private ICollection<byte[]> sketchImages;
		private ICollection<byte[]> designImages;
		private ICollection<byte[]> resultImages;

		public Order()
		{
			this.sketchImages = new HashSet<byte[]>();
			this.designImages = new HashSet<byte[]>();
			this.resultImages = new HashSet<byte[]>();
		}

		[Key]
		public int Id { get; set; }

		public string Title { get; set; }

		public string OrderText { get; set; }

		public DateTime RequestDate { get; set; }

		public DateTime OfferDate { get; set; }

		public DateTime CompleteDate { get; set; }

		public virtual ICollection<byte[]> SketchImages
		{
			get { return this.sketchImages; }
			set { this.sketchImages = value; }
		}

		public virtual ICollection<byte[]> DesignImages
		{
			get { return this.designImages; }
			set { this.designImages = value; }
		}

		public virtual ICollection<byte[]> ResultImages
		{
			get { return this.resultImages; }
			set { this.resultImages = value; }
		}

		public virtual ApplicationUser Client { get; set; }
	}
}
