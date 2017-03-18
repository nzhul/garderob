using System.Web.Mvc;

namespace App.Web.Areas.Administration.Controllers
{
	[Authorize(Roles = "Admin")]
    public class BaseController : Controller
    {

    }
}