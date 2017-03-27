using App.Models;
using System.Collections.Generic;

namespace App.Web.Areas.Administration.Models
{
	public class ClientsMasterModel
	{
		public IEnumerable<ApplicationUser> ActiveUsers { get; set; }

		public IEnumerable<ApplicationUser> InActiveUsers { get; set; }

	}
}