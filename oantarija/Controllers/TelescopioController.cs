using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using oantarija.Models;


namespace oantarija.Controllers
{
    public class TelescopioController : Controller
    {
        oantarijaEntities BD = new oantarijaEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ListarTelescopio()
        {
            string cadena = "";
            cadena = "<table id='data' class='display highlight' cellspacing='0' hidden>";
            cadena += "<thead class='light-blue darken-4 white-text z-depth-3'>";
            cadena += "<tr>";
            cadena += "<th>Nombre</th>";
            cadena += "<th>Marca</th>";
            cadena += "<th>Tipo</th>";
            cadena += "<th>Estado</th>";
            cadena += "<th>Opciones</th>";
            cadena += "</tr>";
            cadena += "</thead>";
            cadena += "<tbody>";
            foreach (var obj in BD.telescopio.ToList())
            {
                cadena += "<tr>";
                cadena += "<td>" + obj.nombre + "</td>";
                cadena += "<td>" + obj.marca + "</td>";
                cadena += "<td>" + obj.tipo + "</td>";
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
                cadena += "<a class='waves-effect waves-light btn btn-floating red'><i class='icon-trash' onclick='ModalConfirmar(" + obj.id + ");'></i></a>";
                cadena += "</td>";
                cadena += "</tr>";
            }
            cadena += "</tbody>";
            cadena += "</table>";
            return Json(cadena, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GuardarTelescopio(int id, string nombre, string marca, string tipo, string diametro, string dis_focal, string montura, bool estado)
        {
            telescopio obj;
            string error = "";


            if (string.IsNullOrEmpty(error))
            {
                if (id == 0)
                {
                    obj = new telescopio();
                    obj.nombre = nombre;
                    obj.marca = marca;
                    obj.tipo = tipo;
                    obj.diametro = diametro;
                    obj.dis_focal = dis_focal;
                    obj.montura = montura;
                    obj.estado = estado;
                    BD.telescopio.Add(obj);
                    BD.SaveChanges();
                }
                else
                {
                    obj = BD.telescopio.Single(o => o.id == id);
                    obj.nombre = nombre;
                    obj.marca = marca;
                    obj.tipo = tipo;
                    obj.diametro = diametro;
                    obj.dis_focal = dis_focal;
                    obj.montura = montura;
                    obj.estado = estado;
                    BD.SaveChanges();
                }
            }

            return Json(error, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetTelescopio(int id)
        {
            telescopio obj = BD.telescopio.Single(o => o.id == id);

            var telescopio = new { nom = obj.nombre, mar = obj.marca, tip = obj.tipo, dia = obj.diametro,dis_f = obj.dis_focal, mon = obj.montura , estado = obj.estado };
            return Json(telescopio, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteTelescopio(int id)
        {
            telescopio obj = BD.telescopio.Single(o => o.id == id);
            BD.telescopio.Remove(obj);
            BD.SaveChanges();
            return Json(null, JsonRequestBehavior.AllowGet);
        }
    }
}