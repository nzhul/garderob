using App.Web.Infrastructure.Bundling;
using System.Web.Optimization;

namespace App.Web
{
	public class BundleConfig
	{
		// For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
		public static void RegisterBundles(BundleCollection bundles)
		{

			bundles.IgnoreList.Clear();

			Bundle mainFrontendStyleBundle = new StyleBundle("~/bundles/garderob-fe-styles").Include(
					"~/Content/css/reset.css",
					"~/Content/css/bootstrap.css",
					"~/Content/css/font-awesome.css",
					"~/Content/css/owl.carousel.css",
					"~/Content/css/jquery.fancybox.css",
					"~/Content/fonts/fi/flaticon.css",
					"~/Content/css/main.css",
					"~/Content/css/indent.css",
					"~/Content/css/garderob.css",
					"~/Content/css/responsive.css"
				);

			mainFrontendStyleBundle.Orderer = new NonOrderingBundleOrderer();

			bundles.Add(mainFrontendStyleBundle);

			Bundle mainFrontendScriptBundle = new ScriptBundle("~/bundles/garderob-fe-scripts").Include(
					"~/Content/js/jquery.min.js",
					"~/Content/js/jquery-ui.min.js",
					"~/Content/js/jquery.validate.min.js",
					"~/Content/js/bootstrap.js",
					"~/Content/js/owl.carousel.js",
					"~/Content/js/jquery.sticky.js",
					"~/Content/js/TweenMax.min.js",
					"~/Content/js/jquery.fancybox.pack.js",
					"~/Content/js/jquery.fancybox-media.js",
					"~/Content/js/isotope.pkgd.min.js",
					"~/Content/js/imagesloaded.pkgd.min.js",
					"~/Content/js/masonry.pkgd.min.js",
					"~/Content/js/jquery.form.min.js",
					"~/Content/js/script.js"
				);

			mainFrontendScriptBundle.Orderer = new NonOrderingBundleOrderer();

			bundles.Add(mainFrontendScriptBundle);

			bundles.Add(new StyleBundle("~/bundles/administration").Include(
						"~/Content/bootstrap.min.css",
						"~/Content/font-awesome.css",
						"~/Content/administration.css"
				));

			bundles.Add(new ScriptBundle("~/bundles/dropzone-scripts").Include(
						"~/Content/dropzone/dropzone.js"));

			bundles.Add(new StyleBundle("~/bundles/dropzone-styles").Include(
						"~/Content/dropzone/basic.css",
						"~/Content/dropzone/dropzone.css"));

			bundles.Add(new ScriptBundle("~/bundles/codemirror-styles").Include(
				"~/Content/codemirror/lib/codemirror.css"
			));

			bundles.Add(new ScriptBundle("~/bundles/codemirror-scripts").Include(
				"~/Content/codemirror/lib/codemirror.js",
				"~/Content/codemirror/mode/xml/xml.js"
			));


			BundleTable.EnableOptimizations = false;
		}
	}
}