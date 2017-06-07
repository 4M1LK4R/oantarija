using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using oantarija.Models;

namespace oantarija.ContBoletinlers
{
    public class BoletinController : Controller
    {
        oantarijaEntities BD = new oantarijaEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ListarBoletines(string usu)
        {
            usuario u;
            if (usu == "admin@admin")
            {
                u = BD.usuario.Single(o => o.id == 1);
            }
            else
            {
                u = BD.usuario.Single(o => o.username == usu);
            }

            string cadena = "";
            cadena = "<table id='data' class='centered scrollable display highlight' cellspacing='0' hidden>";
            cadena += "<thead class='light-blue darken-4 white-text z-depth-3'>";
            cadena += "<tr>";
            cadena += "<th>Nombre</th>";
            cadena += "<th>Fecha</th>";
            cadena += "<th>Enlace</th>";
            if (u.rol == 1)
            {
                cadena += "<th>Estado</th>";
                cadena += "<th>Opciones</th>";
            }
            cadena += "</tr>";
            cadena += "</thead>";
            cadena += "<tbody>";
            foreach (var obj in BD.boletin.ToList())
            {
                cadena += "<tr>";
                cadena += "<td>" + obj.nombre + "</td>";
                cadena += "<td>" + obj.fecha_registro.ToShortDateString() + "</td>";
                cadena += "<td><a class='btn btn-floating cyan pulse' href='"+obj.url+"' target='_blank'><i class='icon-doc-text-inv'></i></a></td>";
                if (u.rol == 1)
                {
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
                }
                cadena += "</tr>";
            }
            cadena += "</tbody>";
            cadena += "</table>";
            return Json(cadena, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GuardarBoletin(int id, string nombre, string url, string descripcion, bool estado)
        {
            boletin obj;
            string error = "";
            if (string.IsNullOrEmpty(nombre))
                error = "El campo nombre esta vacio";
            if (string.IsNullOrEmpty(url))
                error = "El campo url esta vacio";

            if (BD.boletin.ToList().Exists(o => o.nombre == nombre) && id == 0)
                error = "Ya existe un objeto con es nombre";

            if (string.IsNullOrEmpty(error))
            {
                if (id == 0)
                {
                    obj = new boletin();
                    obj.nombre = nombre;
                    obj.descripcion = descripcion;
                    obj.fecha_registro = DateTime.Now.Date;
                    obj.url = url;
                    obj.estado = estado;
                    BD.boletin.Add(obj);
                    BD.SaveChanges();
                }
                else
                {
                    obj = BD.boletin.Single(o => o.id == id);
                    obj.nombre = nombre;
                    obj.descripcion = descripcion;
                    obj.fecha_registro = DateTime.Now.Date;
                    obj.url = url;
                    obj.estado = estado;
                    BD.SaveChanges();
                }
            }

            return Json(error, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetBoletin(int id)
        {
            boletin obj = BD.boletin.Single(o => o.id == id);
            var Boletin = new
            {
                nombre = obj.nombre,
                descripcion = obj.descripcion,
                url = obj.url,
                estado = obj.estado
            };
            return Json(Boletin, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteBoletin(int id)
        {
            try
            {
                boletin obj = BD.boletin.Single(o => o.id == id);
                BD.boletin.Remove(obj);
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