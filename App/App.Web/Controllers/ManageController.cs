using App.Data.Service.Abstraction;
using App.Models;
using App.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Utilities;

namespace App.Web.Controllers
{
	[Authorize]
	public class ManageController : Controller
	{
		private ApplicationSignInManager _signInManager;
		private ApplicationUserManager _userManager;
		private IClientsService clientsService;

		public ManageController(IClientsService clientsService)
		{
			this.clientsService = clientsService;
		}

		public ManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
		{
			UserManager = userManager;
			SignInManager = signInManager;
		}

		public ApplicationSignInManager SignInManager
		{
			get
			{
				return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
			}
			private set
			{
				_signInManager = value;
			}
		}

		public ApplicationUserManager UserManager
		{
			get
			{
				return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
			}
			private set
			{
				_userManager = value;
			}
		}

		//
		// GET: /Manage/Index
		public async Task<ActionResult> Profile()
		{
			string userId = User.Identity.GetUserId();

			UpdateProfileViewModel model = new UpdateProfileViewModel();
			ApplicationUser currentUser = this.clientsService.GetUserById(userId);
			model = this.MapApplicationUserToViewModel(currentUser);


			return View(model);
		}

		private UpdateProfileViewModel MapApplicationUserToViewModel(ApplicationUser currentUser)
		{
			UpdateProfileViewModel viewModel = new UpdateProfileViewModel
			{
				FirstName = currentUser.FirstName,
				LastName = currentUser.LastName,
				Company = currentUser.Company,
				City = currentUser.City,
				Address = currentUser.Address,
				DeliveryAddress = currentUser.DeliveryAddress,
				Phone = currentUser.PhoneNumber,
				InvoiceData = currentUser.InvoiceData,
				ProfileImage = currentUser.ProfileImage
			};

			return viewModel;
		}

		//
		// POST: /Account/Register
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Profile(UpdateProfileViewModel model)
		{
			if (ModelState.IsValid)
			{
				//string currentUserEmail = this.User.Identity.Name;
				ApplicationUser currentUser = this.clientsService.GetUserById(this.User.Identity.GetUserId());
				var user = new ApplicationUser
				{
					UserName = currentUser.UserName,
					Email = currentUser.Email,
					FirstName = model.FirstName,
					LastName = model.LastName,
					Company = model.Company,
					City = model.City,
					Address = model.Address,
					DeliveryAddress = model.DeliveryAddress,
					PhoneNumber = model.Phone,
					InvoiceData = model.InvoiceData,
					ProfileImage = currentUser.ProfileImage
				};

				model.ProfileImage = currentUser.ProfileImage;

				this.clientsService.UpdateClient(user);
				TempData["message"] = "Данните бяха обновени успешно!";
				TempData["messageType"] = "success";
			}

			return View(model);
		}

		[HttpPost]
		public ActionResult Upload()
		{
			if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
			{
				HttpPostedFileBase uploadedImage = new HttpPostedFileWrapper(System.Web.HttpContext.Current.Request.Files["ProfileImage"]);

				if (uploadedImage.IsImage())
				{
					string userId = this.User.Identity.GetUserId(); // TODO: get this from POST data.
					byte[] resizedImage = this.clientsService.UploadProfileImage(uploadedImage, userId);

					return this.Content(string.Format("data:image/jpg;base64,{0}", System.Convert.ToBase64String(resizedImage)));
				}
			}

			return Content("");
		}


		public ActionResult ChangePassword()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}
			var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
			if (result.Succeeded)
			{
				var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
				if (user != null)
				{
					await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
				}
				return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
			}
			AddErrors(result);
			return View(model);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && _userManager != null)
			{
				_userManager.Dispose();
				_userManager = null;
			}

			base.Dispose(disposing);
		}

		#region Helpers
		// Used for XSRF protection when adding external logins
		private const string XsrfKey = "XsrfId";

		private IAuthenticationManager AuthenticationManager
		{
			get
			{
				return HttpContext.GetOwinContext().Authentication;
			}
		}

		private void AddErrors(IdentityResult result)
		{
			foreach (var error in result.Errors)
			{
				ModelState.AddModelError("", error);
			}
		}

		private bool HasPassword()
		{
			var user = UserManager.FindById(User.Identity.GetUserId());
			if (user != null)
			{
				return user.PasswordHash != null;
			}
			return false;
		}

		private bool HasPhoneNumber()
		{
			var user = UserManager.FindById(User.Identity.GetUserId());
			if (user != null)
			{
				return user.PhoneNumber != null;
			}
			return false;
		}

		public enum ManageMessageId
		{
			AddPhoneSuccess,
			ChangePasswordSuccess,
			SetTwoFactorSuccess,
			SetPasswordSuccess,
			RemoveLoginSuccess,
			RemovePhoneSuccess,
			Error
		}

		#endregion
	}
}