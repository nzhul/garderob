using App.Models.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Web.Areas.Administration.Models
{
	public class OrdersMasterModel
	{
		public IEnumerable<OrderViewModelSimple> InProduction { get; set; }

		public IEnumerable<OrderViewModelSimple> WaitingOffer { get; set; }

		public IEnumerable<OrderViewModelSimple> WaitingClientResponse { get; set; }

		public IEnumerable<OrderViewModelSimple> Canceled { get; set; }

		public IEnumerable<OrderViewModelSimple> Done { get; set; }
	}
}