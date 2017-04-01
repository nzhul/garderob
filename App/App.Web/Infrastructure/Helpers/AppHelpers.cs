using System;
using System.Web;
using System.Web.Mvc;

namespace App.Web.Infrastructure.Helpers
{
	public static class AppHelpers
	{
		public static IHtmlString ByteImage(this HtmlHelper helper, byte[] imageData, string className, string id, string title, string alt)
		{
			var base64 = Convert.ToBase64String(imageData);
			var imgSrc = String.Format("data:image/jpg;base64,{0}", base64);

			TagBuilder tb = new TagBuilder("img");
			tb.AddCssClass(className);
			tb.Attributes.Add("src", imgSrc);
			tb.Attributes.Add("alt", alt);
			tb.Attributes.Add("id", id);
			tb.Attributes.Add("data-toggle", "tooltip");
			tb.Attributes.Add("title", title);
			string htmlString = tb.ToString(TagRenderMode.SelfClosing);

			return new HtmlString(htmlString);
		}
	}
}

//<img src = "img/live-preview/materials/small/1-white.jpg" alt="Материал" data-toggle="tooltip" data-material-id="1" title="" data-original-title="Име на материала">