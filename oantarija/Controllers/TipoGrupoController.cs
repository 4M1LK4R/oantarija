using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using oantarija.Models;

namespace oantarija.Controllers
{
    public class TipoGrupoController : Controller
    {
        oantarijaEntities BD = new oantarijaEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ListarTipoGrupo()
        {
            string cadena = "";
            cadena = "<table id='data' class='display highlight' cellspacing='0' hidden>";
            cadena += "<thead class='light-blue darken-4 white-text z-depth-3'>";
            cadena += "<tr>";
            cadena += "<th>Nombre</th>";
            cadena += "<th>Estado</th>";
            cadena += "<th>Opciones</th>";
            cadena += "</tr>";
            cadena += "</thead>";
            cadena += "<tbody>";
            foreach (var obj in BD.tipo_grupo.ToList())
            {
                cadena += "<tr>";
                cadena += "<td>" + obj.nombre + "</td>";
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
        public ActionResult GuardarTipoGrupo(int id, string nombre, bool estado)
        {
            tipo_grupo obj;
            string error = "";
            if (string.IsNullOrEmpty(nombre))
                error = "El campo nombre esta vacio";
           
            if (BD.curso.ToList().Exists(o => o.nombre == nombre) && id == 0)
                error = "Ya existe un objeto con es nombre";

            if (string.IsNullOrEmpty(error))
            {
                if (id == 0)
                {
                    obj = new tipo_grupo();
                    obj.nombre = nombre;
                    obj.estado = estado;
                    BD.tipo_grupo.Add(obj);
                    BD.SaveChanges();
                }
                else
                {
                    obj = BD.tipo_grupo.Single(o => o.id == id);
                    obj.nombre = nombre;
                    obj.estado = estado;
                    BD.SaveChanges();
                }
            }

            return Json(error, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetTipoGrupo(int id)
        {
            tipo_grupo obj = BD.tipo_grupo.Single(o => o.id == id);
            var tipo_grupo = new { nombre = obj.nombre, estado = obj.estado };
            return Json(tipo_grupo, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteTipoGrupo(int id)
        {
            tipo_grupo obj = BD.tipo_grupo.Single(o => o.id == id);
            BD.tipo_grupo.Remove(obj);
            BD.SaveChanges();
            return Json(null, JsonRequestBehavior.AllowGet);
        }
    }
}