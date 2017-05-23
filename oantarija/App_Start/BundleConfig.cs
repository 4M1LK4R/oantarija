using System.Web;
using System.Web.Optimization;

namespace oantarija
{
    public class BundleConfig
    {
        // Para obtener más información sobre Bundles, visite http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // preparado para la producción y podrá utilizar la herramienta de compilación disponible en http://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            //Css MaterializeFontello
            bundles.Add(new StyleBundle("~/MaterializeFontello").Include(
                "~/Resources/plugins/materialize/css/materialize.css",
                "~/Resources/plugins/css/style.css",
                "~/Resources/plugins/fontello/css/fontello.css",
                "~/Resources/plugins/datatable/css/jquery.dataTables.min.css"
                ));
            //Scripts
            bundles.Add(new ScriptBundle("~/JQueryMaterialize").Include(
                "~/Resources/plugins/jquery/jquery-3.2.1.min.js",
                "~/Resources/plugins/jquery/jquery.numeric.js",
                "~/Resources/plugins/materialize/js/materialize.js",
                "~/Resources/plugins/materialize/js/init.js"
                ));
            //"~/Resources/plugins/js/reservar-visita.js"
            bundles.Add(new ScriptBundle("~/bundles/salas").Include(
                "~/Resources/plugins/js/salas.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/tipogrupo").Include(
                "~/Resources/plugins/js/tipogrupo.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/horario").Include(
                "~/Resources/plugins/js/horario.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/tema").Include(
                "~/Resources/plugins/js/tema.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/curso").Include(
                "~/Resources/plugins/js/curso.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/rol").Include(
                "~/Resources/plugins/js/rol.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/boletin").Include(
                "~/Resources/plugins/js/boletin.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/cuenta").Include(
                "~/Resources/plugins/js/cuenta.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/disertante").Include(
                "~/Resources/plugins/js/disertante.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/reserva").Include(
               "~/Resources/plugins/js/reserva.js"
               ));
            bundles.Add(new ScriptBundle("~/bundles/registro_nubosidad").Include(
                "~/Resources/plugins/js/registro_nubosidad.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/telescopio").Include(
                "~/Resources/plugins/js/telescopio.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/camara").Include(
                "~/Resources/plugins/js/camara.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/software").Include(
                "~/Resources/plugins/js/software.js"
                ));

        }
    }
}
