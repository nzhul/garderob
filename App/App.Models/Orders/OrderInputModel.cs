using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace App.Models.Orders
{
	public class OrderInputModel
	{
		public List<HttpPostedFileBase> SketchImages { get; set; }

		public List<HttpPostedFileBase> DesignImages { get; set; }

		public List<HttpPostedFileBase> ResultImages { get; set; }

		[Required(ErrorMessage = " * Задължително!")]
		[Display( Name = "Име на проекта:")]
		public string Title { get; set; }

		[Required(ErrorMessage = " * Задължително!")]
		[Display(Name = "Допълнителна информация:")]
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