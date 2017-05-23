using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using oantarija.Models;

namespace oantarija.Controllers
{
    public class SoftwareController : Controller
    {
        oantarijaEntities BD = new oantarijaEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ListarSoftware()
        {
            string cadena = "";
            cadena = "<table id='data' class='display highlight' cellspacing='0' hidden>";
            cadena += "<thead class='light-blue darken-4 white-text z-depth-3'>";
            cadena += "<tr>";
            cadena += "<th>Nombre</th>";
            cadena += "<th>Objetivo</th>";
            cadena += "<th>Estado</th>";
            cadena += "<th>Opciones</th>";
            cadena += "</tr>";
            cadena += "</thead>";
            cadena += "<tbody>";
            foreach (var obj in BD.software.ToList())
            {
                cadena += "<tr>";
                cadena += "<td>" + obj.nombre + "</td>";
                cadena += "<td>" + obj.objetivo + "</td>";
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
        public ActionResult GuardarSoftware(int id, string nombre, string objetivo, string descripcion, bool estado)
        {
            software obj;
            string error = "";


            if (string.IsNullOrEmpty(error))
            {
                if (id == 0)
                {
                    obj = new software();
                    obj.nombre = nombre;
                    obj.objetivo = objetivo;
                    obj.descripcion = descripcion;
                    obj.estado = estado;
                    BD.software.Add(obj);
                    BD.SaveChanges();
                }
                else
                {
                    obj = BD.software.Single(o => o.id == id);
                    obj.nombre = nombre;
                    obj.objetivo = objetivo;
                    obj.descripcion = descripcion;
                    obj.estado = estado;
                    BD.SaveChanges();
                }
            }

            return Json(error, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetSoftware(int id)
        {
            software obj = BD.software.Single(o => o.id == id);

            var software = new { nom = obj.nombre, obje = obj.objetivo, des = obj.descripcion, estado = obj.estado };
            return Json(software, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteSoftware(int id)
        {
            software obj = BD.software.Single(o => o.id == id);
            BD.software.Remove(obj);
            BD.SaveChanges();
            return Json(null, JsonRequestBehavior.AllowGet);
        }
    }
}