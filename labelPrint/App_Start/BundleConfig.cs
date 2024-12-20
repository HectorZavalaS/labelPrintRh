using System.Web;
using System.Web.Optimization;

namespace labelPrint
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/interfaces").Include(
                      "~/Scripts/jquery.blockUI.js",
                      //"~/Scripts/chosen.jquery.js",
                      "~/Scripts/DataTables/jquery.dataTables.js",
                      "~/Scripts/DataTables/dataTables.scroller.min.js",
                      "~/Scripts/DataTables/dataTables.bootstrap.js",
                      "~/Scripts/metisMenu.js",
                      "~/Scripts/jquery.validationEngine-es.js",
                      "~/Scripts/jquery.validationEngine.js",
                      "~/Scripts/bootstrap-dialog.js",
                      "~/Scripts/bootstrap-select.js",
                      "~/Scripts/interfaces.js",
                      "~/Scripts/labelsTwoLed.js",///labelsTwoLed
                      "~/Scripts/gui.js",
                      "~/Scripts/laserMark.js",
                      "~/Scripts/adminModels.js",
                      "~/Scripts/ftp.js",
                      "~/Scripts/laserMark.js",
                      "~/Content/assets/scripts/main.js")); 


            bundles.Add(new ScriptBundle("~/bundles/init").Include(
                      "~/Scripts/IniApp.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/main.css",
                      //"~/Content/chosen.css",
                      //"~/Content/font-awesome.min.css",
                      "~/Content/dashboard.css",
                      "~/Content/site.css",
                      "~/Content/style_light.css",
                      "~/Content/metisMenu.css",
                      "~/Content/validationEngine.jquery.css",
                      "~/Content/bootstrap-dialog.css",
                      "~/Content/bootstrap-select.css",
                      "~/Content/progressbar.css",
                      "~/Content/DataTables/css/dataTables.bootstrap.css"));
        }
    }
}