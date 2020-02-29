using System.Web;
using System.Web.Optimization;

namespace SalesTrackingSystem
{
    public class BundleConfig
    {      
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                   "~/Assets/vendor/jquery-3.2.1.min.js",
                   "~/Assets/vendor/bootstrap-4.1/popper.min.js",
                   "~/Assets/vendor/bootstrap-4.1/bootstrap.min.js",
                   "~/Assets/vendor/slick/slick.min.js",
                   "~/Assets/vendor/wow/wow.min.js",
                   "~/Assets/vendor/animsition/animsition.min.js",
                   "~/Assets/vendor/bootstrap-progressbar/bootstrap-progressbar.min.js",
                   "~/Assets/vendor/counter-up/jquery.waypoints.min.js",
                   "~/Assets/vendor/counter-up/jquery.counterup.min.js",
                   "~/Assets/vendor/circle-progress/circle-progress.min.js",
                   "~/Assets/vendor/perfect-scrollbar/perfect-scrollbar.js",
                   "~/Assets/vendor/chartjs/Chart.bundle.min.js",
                   "~/Assets/vendor/select2/select2.min.js",
                   "~/Assets/vendor/tinymce/tinymce.min.js",
                   "~/Assets/js/main.js",
                   "~/Assets/vendor/dropify/js/dropify.min.js",
                   "~/Assets/vendor/data-tables/dataTables.min.js",
                   "~/Assets/vendor/data-tables/extensions/responsive/js/dataTables.responsive.min.js",
                   "~/Assets/js/custom.js"                
                ));
            bundles.Add(new ScriptBundle("~/bundles/detect/js").Include(                  
                   "~/Assets/js/detect.js"
                ));

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                 "~/Assets/css/font-face.css",
                 "~/Assets/vendor/font-awesome-4.7/css/font-awesome.min.css",
                 "~/Assets/vendor/font-awesome-5/css/fontawesome-all.min.css",
                 "~/Assets/vendor/mdi-font/css/material-design-iconic-font.min.css",
                 "~/Assets/vendor/bootstrap-4.1/bootstrap.min.css",
                 "~/Assets/vendor/animsition/animsition.min.css",
                 "~/Assets/vendor/bootstrap-progressbar/bootstrap-progressbar-3.3.4.min.css",
                 "~/Assets/vendor/wow/animate.css",
                 "~/Assets/vendor/css-hamburgers/hamburgers.min.css",
                 "~/Assets/vendor/slick/slick.css",
                 "~/Assets/vendor/select2/select2.min.css",
                 "~/Assets/vendor/perfect-scrollbar/perfect-scrollbar.css",
                 "~/Assets/vendor/data-tables/extensions/responsive/css/responsive.dataTables.min.css",
                 "~/Assets/vendor/data-tables/dataTables.css",
                 "~/Assets/vendor/data-tables/dataTables.min.css",
                 "~/Assets/vendor/dropify/css/dropify.min.css",
                 "~/Assets/css/theme.css"
               ));

            bundles.Add(new StyleBundle("~/bundles/error").Include(
                "~/Assets/css/error.css"
              ));

            BundleTable.EnableOptimizations = false;           
        }
    }
}
