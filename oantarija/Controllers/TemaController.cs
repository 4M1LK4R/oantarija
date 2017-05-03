using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using oantarija.Models;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace oantarija.Controllers
{
    public class temaController : Controller
    {
        oantarijaEntities BD = new oantarijaEntities();
        // GET: tema
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ListarTemas()
        {
            string cadena = "";
            cadena = "<table id='data' class='display highlight' cellspacing='0' hidden>";
            cadena += "<thead class='light-blue darken-4 white-text z-depth-3'>";
            cadena += "<tr>";
            cadena += "<th>Nombre</th>";
            cadena += "<th>Disertante</th>";
            cadena += "<th>Sala</th>";
            cadena += "<th>Estado</th>";
            cadena += "<th>Opciones</th>";
            cadena += "</tr>";
            cadena += "</thead>";
            cadena += "<tbody>";
            foreach (var obj in BD.tema.ToList())
            {
                cadena += "<tr>";
                cadena += "<td>" + obj.nombre + "</td>";
                cadena += "<td>" + obj.disertante1.persona.nombre + " "+ obj.disertante1.persona.apellido + "</td>";
                cadena += "<td>" + obj.sala1.nombre + "</td>";
                //cadena += "<td>" + obj.fecharegistro + "</td>";
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
        public ActionResult ListarDisertantes()
        {
            string cadena = "<p class='left-align'>Disertante:</p>";
            cadena += "<select id='selectDisertante'>";
            cadena += "<option value='' disabled selected>(Seleccionar disertante)</option>";
            foreach (var obj in BD.disertante.ToList().Where(o => o.persona.estado).OrderBy(o=>o.persona.nombre))
            {
                cadena += "<option value=" + obj.id + ">" + obj.persona.nombre+" "+obj.persona.apellido+ "</option>";
            }
            cadena += "</select>";
            return Json(cadena,JsonRequestBehavior.AllowGet);
        }
        public ActionResult ListarSalas()
        {
            string cadena = "<p class='left-align'>Sala:</p>";
            cadena += "<select id='selectSala'>";
            cadena += "<option value='' disabled selected>(Seleccionar sala)</option>";
            foreach (var obj in BD.sala.ToList().Where(o => o.estado).OrderBy(o=>o.nombre))
            {
                cadena += "<option value=" + obj.id + ">" + obj.nombre +"</option>";
            }
            cadena += "</select>";
            return Json(cadena, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GuardarTema(int id, string nombre, string descripcion, int disertante, int sala, bool estado)
        {
            tema obj;
            string error = "";
            if (string.IsNullOrEmpty(nombre))
                error = "El campo nombre esta vacio";

            if (string.IsNullOrEmpty(descripcion))
                error = "El campo descripcion esta vacio";

            if (BD.tema.ToList().Exists(o => o.nombre == nombre) && id == 0)
                error = "Ya existe un objeto con es nombre";


            if (string.IsNullOrEmpty(error))
            {
                if (id == 0)
                {
                    obj = new tema();
                    obj.nombre = nombre;
                    obj.descripcion = descripcion;
                    obj.disertante = disertante;
                    obj.sala = sala;
                    obj.estado = estado;
                    BD.tema.Add(obj);
                    BD.SaveChanges();
                }
                else
                {
                    obj = BD.tema.Single(o => o.id == id);
                    obj.nombre = nombre;
                    obj.descripcion = descripcion;
                    obj.disertante = disertante;
                    obj.sala = sala;
                    obj.estado = estado;
                    BD.SaveChanges();
                }
            }

            return Json(error, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetTema(int id)
        {
            tema obj = BD.tema.Single(o => o.id == id);
            var tema = new { nombre = obj.nombre, descripcion = obj.descripcion, estado = obj.estado };
            return Json(tema, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteTema(int id)
        {
            tema obj = BD.tema.Single(o => o.id == id);
            BD.tema.Remove(obj);
            BD.SaveChanges();
            return Json(null, JsonRequestBehavior.AllowGet);
        }
    }
}