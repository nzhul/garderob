using App.Models;
using App.Web.Models;
using System.Configuration;
using System.Net.Mail;
using System.Web.Mvc;
using System.Linq;
using System.Globalization;
using App.Data;

namespace App.Web.Controllers
{
	public class HomeController : Controller
	{
		private IUoWData data;

		public HomeController(IUoWData data)
		{
			this.data = data;
		}

		public ActionResult Index()
		{
			return View();
		}

		public ActionResult Contact()
		{
			var model = new ContactFormInputModel();
			return View(model);
		}

		[HttpPost]
		public ActionResult Contact(ContactFormInputModel contactData)
		{
			if (Request.IsAuthenticated)
			{
				// Get the userData from the database and use it to populate the email
				string currentUserEmail = this.User.Identity.Name;
				ApplicationUser currentUser = this.data.Users.All().Where(u => u.Email == currentUserEmail).FirstOrDefault();
				if (currentUser != null)
				{
					contactData.Email = currentUser.Email;
					contactData.Phone = currentUser.PhoneNumber;
					contactData.Name = currentUser.FirstName + " " + currentUser.LastName;

					ModelState.SetModelValue("Name", new ValueProviderResult(currentUser.FirstName + " " + currentUser.LastName, "", CultureInfo.InvariantCulture));
					ModelState.SetModelValue("Phone", new ValueProviderResult(currentUser.PhoneNumber, "", CultureInfo.InvariantCulture));
					ModelState.SetModelValue("Email", new ValueProviderResult(currentUser.Email, "", CultureInfo.InvariantCulture));

					ModelState["Email"].Errors.Clear();
					ModelState["Name"].Errors.Clear();
					ModelState["Phone"].Errors.Clear();
				}
			}

			if (TryValidateModel(contactData)) // try validate is failing for some reason. Find out why
			{
				string sender = ConfigurationManager.AppSettings["emailSender"];
				string receiver = ConfigurationManager.AppSettings["emailReceiver"];

				MailMessage mailMessage = new MailMessage(sender, receiver);
				mailMessage.IsBodyHtml = true;
				mailMessage.Subject = "Запитване (контактна форма): ";
				mailMessage.Body = "Имена: " + contactData.Name + "<br/>" +
								   "Email: " + contactData.Email + "<br/>" +
								   "Телефон: " + contactData.Phone + "<br/><br/>" +
								   "Запитване: <br/>" + contactData.Content;

				SmtpClient smtpClient = new SmtpClient();

				// The settings are in web.config file
				smtpClient.Send(mailMessage);

				return Json(new { Status = "Success" });
			}
			else
			{
				return Json(new { Status = "InvalidModel" });
			}
		}
	}
}