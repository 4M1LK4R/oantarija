using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using oantarija.Models;

namespace oantarija.Controllers
{
    public class CamaraController : Controller
    {
        oantarijaEntities BD = new oantarijaEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ListarCamara()
        {
            string cadena = "";
            cadena = "<table id='data' class='display highlight' cellspacing='0' hidden>";
            cadena += "<thead class='light-blue darken-4 white-text z-depth-3'>";
            cadena += "<tr>";
            cadena += "<th>Nombre</th>";
            cadena += "<th>Marca</th>";
            cadena += "<th>Dimension de Chip</th>";
            cadena += "<th>Estado</th>";
            cadena += "<th>Opciones</th>";
            cadena += "</tr>";
            cadena += "</thead>";
            cadena += "<tbody>";
            foreach (var obj in BD.camara.ToList())
            {
                cadena += "<tr>";
                cadena += "<td>" + obj.nombre + "</td>";
                cadena += "<td>" + obj.marca + "</td>";
                cadena += "<td>" + obj.dim_chip + "</td>";
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
        public ActionResult GuardarCamara(int id, string nombre, string marca, string dim_chip, bool estado)
        {
            camara obj;
            string error = "";


            if (string.IsNullOrEmpty(error))
            {
                if (id == 0)
                {
                    obj = new camara();
                    obj.nombre = nombre;
                    obj.marca = marca;
                    obj.dim_chip = dim_chip;
                    obj.estado = estado;
                    BD.camara.Add(obj);
                    BD.SaveChanges();
                }
                else
                {
                    obj = BD.camara.Single(o => o.id == id);
                    obj.nombre = nombre;
                    obj.marca = marca;
                    obj.dim_chip = dim_chip;
                    obj.estado = estado;
                    BD.SaveChanges();
                }
            }

            return Json(error, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetCamara(int id)
        {
            camara obj = BD.camara.Single(o => o.id == id);

            var camara = new { nom = obj.nombre, mar = obj.marca, dim_ch = obj.dim_chip, estado = obj.estado };
            return Json(camara, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteCamara(int id)
        {
            camara obj = BD.camara.Single(o => o.id == id);
            BD.camara.Remove(obj);
            BD.SaveChanges();
            return Json(null, JsonRequestBehavior.AllowGet);
        }
    }
}