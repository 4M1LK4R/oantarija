using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using oantarija.Models;

namespace oantarija.Controllers
{
    public class SalaController : Controller
    {
        oantarijaEntities BD = new oantarijaEntities();
        // GET: Sala
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ListarSalas()
        {
            string cadena = "";
            cadena = "<table id='data' class='centered scrollable display highlight' cellspacing='0' hidden>";
            cadena += "<thead class='light-blue darken-4 white-text z-depth-3'>";
            cadena += "<tr>";
            cadena += "<th>Nombre</th>";
            cadena += "<th>Capacidad</th>";
            cadena += "<th>Estado</th>";
            cadena += "<th>Opciones</th>";
            cadena += "</tr>";
            cadena += "</thead>";
            cadena += "<tbody>";
            foreach (var obj in BD.sala.ToList())
            {
                cadena += "<tr>";
                cadena += "<td>" + obj.nombre + "</td>";
                cadena += "<td>" + obj.capacidad + "</td>";
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
                cadena += "<a class='waves-effect waves-light btn btn-floating red'><i class='icon-trash' onclick='ModalConfirmar(" + obj.id + ",\"" + obj.nombre + "\");'></i></a>";
                cadena += "</td>";
                cadena += "</tr>";
            }
            cadena += "</tbody>";
            cadena += "</table>";
            return Json(cadena, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GuardarSala(int id, string nombre, int capacidad, bool estado)
        {
            sala obj;
            string error = "";
            if (string.IsNullOrEmpty(nombre))
                error = "El campo nombre esta vacio";

            if (string.IsNullOrEmpty(capacidad.ToString()))
                error = "El campo cantidad esta vacio";

            if (BD.sala.ToList().Exists(o => o.nombre == nombre) && id == 0)
                error = "Ya existe un objeto con es nombre";


            if (string.IsNullOrEmpty(error))
            {
                if (id == 0)
                {
                    obj = new sala();
                    obj.nombre = nombre;
                    obj.capacidad = capacidad;
                    obj.estado = estado;
                    BD.sala.Add(obj);
                    BD.SaveChanges();
                }
                else
                {
                    obj = BD.sala.Single(o => o.id == id);
                    obj.nombre = nombre;
                    obj.capacidad = capacidad;
                    obj.estado = estado;
                    BD.SaveChanges();
                }
            }

            return Json(error, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetSala(int id)
        {
            sala obj = BD.sala.Single(o => o.id == id);
            var sala = new { nombre = obj.nombre, capacidad = obj.capacidad, estado = obj.estado };
            return Json(sala, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteSala(int id)
        {
            try
            {
                sala obj = BD.sala.Single(o => o.id == id);
                BD.sala.Remove(obj);
                BD.SaveChanges();
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }

        }
    }
}