﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using oantarija.Models;

namespace oantarija.Controllers
{
    public class RegistroNubosidadController : Controller
    {
        oantarijaEntities BD = new oantarijaEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ListarRegisNub()
        {
            string cadena = "";
            cadena = "<table id='data' class='centered scrollable display highlight' cellspacing='0' hidden>";
            cadena += "<thead class='light-blue darken-4 white-text z-depth-3'>";
            cadena += "<tr>";
            cadena += "<th>Fecha</th>";
            cadena += "<th>Nubosidad</th>";
            cadena += "<th>Temperatura</th>";
            cadena += "<th>Estado</th>";
            cadena += "<th>Opciones</th>";
            cadena += "</tr>";
            cadena += "</thead>";
            cadena += "<tbody>";
            foreach (var obj in BD.registro_nubosidad.ToList())
            {
                cadena += "<tr>";
                cadena += "<td>" + obj.fecha.Date + "</td>";
                cadena += "<td>" + obj.nubosidad + "</td>";
                cadena += "<td>" + obj.temperatura + "</td>";
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
        public ActionResult GuardarRegisNub(int id, string nubosidad, string temperatura, string observaciones, bool estado)
        {
            registro_nubosidad obj;
            string error = "";


            if (string.IsNullOrEmpty(error))
            {
                if (id == 0)
                {
                    obj = new registro_nubosidad();
                    obj.fecha = DateTime.Now.Date;
                    obj.nubosidad = nubosidad;
                    obj.temperatura = temperatura;
                    obj.observaciones = observaciones;
                    obj.estado = estado;
                    BD.registro_nubosidad.Add(obj);
                    BD.SaveChanges();
                }
                else
                {
                    obj = BD.registro_nubosidad.Single(o => o.id == id);
                    obj.fecha = DateTime.Now.Date;
                    obj.nubosidad = nubosidad;
                    obj.temperatura = temperatura;
                    obj.observaciones = observaciones;
                    obj.estado = estado;
                    BD.SaveChanges();
                }
            }

            return Json(error, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetRegisNub(int id)
        {
            registro_nubosidad obj = BD.registro_nubosidad.Single(o => o.id == id);

            var registro_nubosidad = new { fech = obj.fecha, nub = obj.nubosidad, tem = obj.temperatura, obs = obj.observaciones, estado = obj.estado };
            return Json(registro_nubosidad, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteRegisNub(int id)
        {
            try
            {
                registro_nubosidad obj = BD.registro_nubosidad.Single(o => o.id == id);
                BD.registro_nubosidad.Remove(obj);
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