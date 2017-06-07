using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using oantarija.Models;

namespace oantarija.Controllers
{
    public class VisitaController : Controller
    {
        oantarijaEntities BD = new oantarijaEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Listar()
        {
            string cadena = "";
            cadena = "<table id='data' class='centered scrollable display highlight' cellspacing='0' hidden>";
            cadena += "<thead class='light-blue darken-4 white-text z-depth-3'>";
            cadena += "<tr>";
            cadena += "<th>Fecha</th>";
            cadena += "<th>Cantidad</th>";
            cadena += "<th>Proposito</th>";
            cadena += "<th>Procedencia</th>";
            cadena += "<th>Estado</th>";
            cadena += "<th>Opciones</th>";
            cadena += "</tr>";
            cadena += "</thead>";
            cadena += "<tbody>";
            foreach (var obj in BD.visita.ToList())
            {
                cadena += "<tr>";
                cadena += "<td>" + obj.fecha_registro.ToShortDateString() + "</td>";
                cadena += "<td>" + obj.cantidad_personas + "</td>";
                cadena += "<td>" + obj.proposito + "</td>";
                cadena += "<td>" + obj.procedencia + "</td>";
                if (obj.estado)
                {
                    cadena += "<td>Activo</td>";
                }
                else
                {
                    cadena += "<td>Inactivo</td>";
                }
                cadena += "<td>";
                cadena += "<a class='waves-effect waves-light btn btn-floating blue'><i class='icon-pencil-1' onclick='Editar(" + obj.id + ");'></i></a>&nbsp;";
                cadena += "<a class='waves-effect waves-light btn btn-floating red'><i class='icon-trash' onclick='ModalConfirmar(" + obj.id + ",\"" + obj.id + "\");'></i></a>";
                cadena += "</td>";
                cadena += "</tr>";
            }
            cadena += "</tbody>";
            cadena += "</table>";
            return Json(cadena, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Guardar(int id, string hora_entrada, string hora_salida, int cantidad, string proposito, bool vehiculo, string tipo_v, string placa_v, string color_v, string procedencia, string usuario, bool estado)
        {
            visita obj;
            string error = "";

            if (string.IsNullOrEmpty(cantidad.ToString()))
                error = "El campo cantidad esta vacio";
            if (string.IsNullOrEmpty(proposito))
                error = "El campo proposito esta vacio";
            if (string.IsNullOrEmpty(procedencia))
                error = "El campo procedencia esta vacio";

            if (string.IsNullOrEmpty(error))
            {

                if (!BD.visita.ToList().Exists(o=>o.id==id))
                {
                    obj = new visita();
                    obj.id = id;
                    obj.fecha_registro = DateTime.Now;
                    obj.hora_entrada = TimeSpan.Parse(hora_entrada);
                    obj.hora_salida = TimeSpan.Parse(hora_salida);
                    obj.cantidad_personas = cantidad;
                    obj.proposito = proposito;
                    obj.vehiculo = vehiculo;
                    obj.placa_vehiculo = placa_v;
                    obj.tipo_vehiculo = tipo_v;
                    obj.color_vehiculo = color_v;
                    obj.procedencia = procedencia;
                    obj.usuario = id_usu(usuario);
                    obj.estado = estado;
                    BD.visita.Add(obj);
                    BD.SaveChanges();
                }
                else
                {
                    obj = BD.visita.Single(o=>o.id==id);
                    obj.fecha_registro = DateTime.Now;
                    obj.hora_entrada = TimeSpan.Parse(hora_entrada);
                    obj.hora_salida = TimeSpan.Parse(hora_salida);
                    obj.cantidad_personas = cantidad;
                    obj.proposito = proposito;
                    obj.vehiculo = vehiculo;
                    obj.placa_vehiculo = placa_v;
                    obj.tipo_vehiculo = tipo_v;
                    obj.color_vehiculo = color_v;
                    obj.procedencia = procedencia;
                    obj.usuario = id_usu(usuario);
                    obj.estado = estado;
                    BD.SaveChanges();
                }                
            }

            return Json(error, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Get(int id)
        {
            if (!BD.visita.ToList().Exists(o=>o.id==id))
            {
                return Json("vacio", JsonRequestBehavior.AllowGet);
            }
            else
            {
                visita obj = BD.visita.Single(o => o.id == id);
                var visita = new
                {
                    fecha_registro = obj.fecha_registro,
                    hora_entrada = CambiarHora(obj.hora_entrada.ToString()),
                    hora_salida = CambiarHora(obj.hora_salida.ToString()),
                    cantidad_personas = obj.cantidad_personas,
                    proposito = obj.proposito,
                    vehiculo = obj.vehiculo,
                    placa_vehiculo = obj.placa_vehiculo,
                    tipo_vehiculo = obj.tipo_vehiculo,
                    color_vehiculo = obj.color_vehiculo,
                    procedencia = obj.procedencia,
                    usuario = obj.usuario,
                    estado = obj.estado
                };
                return Json(visita, JsonRequestBehavior.AllowGet);
            }


        }
        public string CambiarHora(string hora)
        {
            string h = "";
            string[] split = hora.Split(new Char[] { ':' });

            for (int i = 0; i < split.Length - 1; i++)
            {
                h += split[i];
                if (i == 0)
                    h += ":";
            }
            return h;
        }
        public ActionResult Delete(int id)
        {
            try
            {
                visita obj = BD.visita.Single(o => o.id == id);
                BD.visita.Remove(obj);
                BD.SaveChanges();
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }

        }

        private int id_usu(string usuario)
        {
            int id = 0;
            if (usuario == "admin@admin")
            {
                id = 1;
            }
            else
            {
                id = BD.usuario.Single(o => o.username == usuario).id;
            }
            return id;
        }
    }
}