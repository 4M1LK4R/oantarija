using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using oantarija.Models;

namespace oantarija.Controllers
{
    public class DisertanteController : Controller
    {
        oantarijaEntities BD = new oantarijaEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ListarDisertantes()
        {
            string cadena = "";
            cadena = "<table id='data' class='centered scrollable display highlight' cellspacing='0' hidden>";
            cadena += "<thead class='light-blue darken-4 white-text z-depth-3'>";
            cadena += "<tr>";
            cadena += "<th>Nombre</th>";
            cadena += "<th>Apellido</th>";
            cadena += "<th>Estado</th>";
            cadena += "<th>Opciones</th>";
            cadena += "</tr>";
            cadena += "</thead>";
            cadena += "<tbody>";
            foreach (var obj in BD.disertante.ToList())
            {
                cadena += "<tr>";
                cadena += "<td>" + obj.persona.nombre + "</td>";
                cadena += "<td>" + obj.persona.apellido + "</td>";
                if (obj.persona.estado)
                {
                    cadena += "<td>Activo</td>";
                }
                else
                {
                    cadena += "<td>Inactivo</td>";
                }
                cadena += "<td>";
                cadena += "<a class='waves-effect waves-light btn btn-floating blue'><i class='icon-pencil-1' onclick='Editar(" + obj.id + ");'></i></a>&nbsp;";
                cadena += "<a class='waves-effect waves-light btn btn-floating red'><i class='icon-trash' onclick='ModalConfirmar(" + obj.id + ",\"" + obj.persona.nombre + "\");'></i></a>";
                cadena += "</td>";
                cadena += "</tr>";
            }
            cadena += "</tbody>";
            cadena += "</table>";
            return Json(cadena, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GuardarDisertante(int id, string nombre, string apellido, bool estado)
        {
            persona obj_p;
            disertante obj;
            string error = "";
            if (string.IsNullOrEmpty(nombre))
                error = "El campo nombre esta vacio";

            if (BD.disertante.ToList().Exists(o => o.persona.nombre == nombre) && id == 0)
                error = "Ya existe un objeto con es nombre";

            if (string.IsNullOrEmpty(error))
            {
                if (id == 0)
                {
                    obj_p = new persona();
                    obj_p.nombre = nombre;
                    obj_p.apellido = apellido;
                    obj_p.estado = estado;
                    BD.persona.Add(obj_p);
                    BD.SaveChanges();

                    obj = new disertante();
                    obj.fecharegistro = DateTime.Now;
                    obj.id = obj_p.id;
                    BD.disertante.Add(obj);
                    BD.SaveChanges();
                }
                else
                {
                    obj_p = BD.persona.Single(o => o.id == id);
                    obj_p.nombre = nombre;
                    obj_p.apellido = apellido;
                    obj_p.estado = estado;
                    BD.SaveChanges();
                }
            }

            return Json(error, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetDisertante(int id)
        {
            disertante obj = BD.disertante.Single(o => o.id == id);
            var disertante = new { nombre = obj.persona.nombre, apellido = obj.persona.apellido, estado = obj.persona.estado };
            return Json(disertante, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteDisertante(int id)
        {
            try
            {
                disertante obj = BD.disertante.Single(o => o.id == id);
                BD.disertante.Remove(obj);
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