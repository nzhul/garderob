using App.Models.Materials;
using App.Models.ValidationAttributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace App.Models.Orders
{
	public class OrderInputModel
	{
		[RequireImageFile(MissingFileErrorMessage = " * Задължително качете поне една скица!", InvalidFileErrorMessage = " * Невалиден файл! Позволени са само: .jpg, .png и .gif")]
		public List<HttpPostedFileBase> PostedSketches { get; set; }

		public IList<Material> SurfaceMaterials { get; set; }

		public IList<Material> FazerMaterials { get; set; }

		public IList<Material> HandlesMaterials { get; set; }

		[Required(ErrorMessage = " * Задължително!")]
		[Display( Name = "Име на проекта:")]
		[StringLength(250, MinimumLength = 3, ErrorMessage = " *Невалидна дължина!")]
		public string Title { get; set; }

		[Display(Name = "Допълнителна информация:")]
		[StringLength(2500, MinimumLength = 3, ErrorMessage = " *Невалидна дължина!")]
		[DataType(DataType.MultilineText)]
		public string OrderText { get; set; }

		[Required(ErrorMessage = " * Задължително!")]
		[Range(1, int.MaxValue, ErrorMessage = " * Задължително!")]
		public int BaseMaterialId { get; set; }

		[Required(ErrorMessage = " * Задължително!")]
		[Range(1, int.MaxValue, ErrorMessage = " * Задължително!")]
		public int DoorsMaterialId { get; set; }

		[Required(ErrorMessage = " * Задължително!")]
		[Range(1, int.MaxValue, ErrorMessage = " * Задължително!")]
		public int FazerMaterialId { get; set; }

		[Required(ErrorMessage = " * Задължително!")]
		[Range(1, int.MaxValue, ErrorMessage = " * Задължително!")]
		public int HandlesMaterialId { get; set; }

		public string ClientId { get; set; }

		[Display(Name = "Email:")]
		[Required(ErrorMessage = " Моля предоставете валиден email!")]
		[DataType(DataType.EmailAddress)]
		[EmailAddress]
		public string AnonymousClientEmail { get; set; }

		[Required(ErrorMessage = " Моля попълнете вашето собствено и фамилно име!")]
		[StringLength(250, MinimumLength = 3, ErrorMessage = " Максимална дължина 250 символа, минимална 3")]
		[Display(Name = "Имена:")]
		public string AnonymousClientName { get; set; }

		[Display(Name = "Телефон:")]
		[Required(ErrorMessage = " Моля предоставете валиден телефонен номер!")]
		[StringLength(250, MinimumLength = 3, ErrorMessage = " Максимална дължина 250 символа, минимална 3")]
		public string AnonymousClientPhone { get; set; }

		public int OrderCategoryId { get; set; } // Pass default value of 0 = "Other"
	}
}

//http://stackoverflow.com/questions/21677038/mvc-upload-file-with-model-second-parameter-posted-file-is-null

//public class UploadFileModel
//{
//	public UploadFileModel()
//	{
//		Files = new List<HttpPostedFileBase>();
//	}

//	public List<HttpPostedFileBase> Files { get; set; }
//	public string FirstName { get; set; }
//	// Rest of model details
//}

//@using(Html.BeginForm("UploadFile", "Home", FormMethod.Post, new { encType="multipart/form-data" }))
//{
//    @Html.TextBoxFor(m => m.FirstName)
//    <br /><br />

//    @Html.TextBoxFor(m => m.Files, new { type = "file", name = "Files" })<br /><br />
//    <input type = "submit" value="submit me" name="submitme" id="submitme" />
//}

//public ActionResult UploadFile(UploadFileModel model)
//{
//	var file = model.Files[0];
//	return View(model);
//}