using App.Models.Materials;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace App.Models.Orders
{
	public class OrderInputModel
	{
		public List<HttpPostedFileBase> PostedSketches { get; set; }

		public IList<Material> SurfaceMaterials { get; set; }

		public IList<Material> HandlesMaterials { get; set; }

		[Required(ErrorMessage = " * Задължително!")]
		[Display( Name = "Име на проекта:")]
		[StringLength(250, MinimumLength = 3, ErrorMessage = "Невалидно име - Максимална дължина 250 символа, минимална 3")]
		public string Title { get; set; }

		[Required(ErrorMessage = " * Задължително!")]
		[Display(Name = "Допълнителна информация:")]
		[StringLength(2500, MinimumLength = 3, ErrorMessage = "Невалидно име - Максимална дължина 2500 символа, минимална 3")]
		[DataType(DataType.MultilineText)]
		public string OrderText { get; set; }

		[Required(ErrorMessage = " * Задължително!")]
		public int BaseMaterialId { get; set; }

		[Required(ErrorMessage = " * Задължително!")]
		public int DoorsMaterialId { get; set; }

		[Required(ErrorMessage = " * Задължително!")]
		public int FazerMaterialId { get; set; }

		[Required(ErrorMessage = " * Задължително!")]
		public int HandlesMaterialId { get; set; }

		[Required]
		public string ClientId { get; set; }

		[Required]
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