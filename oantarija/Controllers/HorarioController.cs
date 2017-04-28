using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Timers;
using oantarija.Models;

namespace oantarija.Controllers
{
    public class HorarioController : Controller
    {
        oantarijaEntities BD = new oantarijaEntities();
        // GET: horario
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ListarHorarios()
        {
            string cadena = "";
            cadena = "<table id='data' class='display highlight' cellspacing='0' hidden>";
            cadena += "<thead class='light-blue darken-4 white-text z-depth-3'>";
            cadena += "<tr>";
            cadena += "<th>Nombre</th>";
            cadena += "<th>Hr. Inicio</th>";
            cadena += "<th>Hr. Fin</th>";
            cadena += "<th>Estado</th>";
            cadena += "<th>Opciones</th>";
            cadena += "</tr>";
            cadena += "</thead>";
            cadena += "<tbody>";
            foreach (var obj in BD.horario.ToList())
            {
                cadena += "<tr>";
                cadena += "<td>" + obj.nombre + "</td>";
                cadena += "<td>" + obj.horainicio + "</td>";
                cadena += "<td>" + obj.horafin + "</td>";
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
        public ActionResult GuardarHorario(int id, string nombre, string h_inicio, string h_fin, bool estado)
        {
            horario obj;
            string error = "";
            if (string.IsNullOrEmpty(nombre))
                error = "El campo nombre esta vacio";

            if (string.IsNullOrEmpty(h_inicio))
                error = "El campo hora inicio esta vacio";

            if (string.IsNullOrEmpty(h_fin))
                error = "El campo hora fin esta vacio";

            if (BD.horario.ToList().Exists(o => o.nombre == nombre) && id == 0)
                error = "Ya existe un objeto con es nombre";


            if (string.IsNullOrEmpty(error))
            {
                if (id == 0)
                {
                    obj = new horario();
                    obj.nombre = nombre;
                    obj.horainicio = TimeSpan.Parse(h_inicio);
                    obj.horafin = TimeSpan.Parse(h_fin);
                    obj.estado = estado;
                    BD.horario.Add(obj);
                    BD.SaveChanges();
                }
                else
                {
                    obj = BD.horario.Single(o => o.id == id);
                    obj.nombre = nombre;
                    obj.horainicio = TimeSpan.Parse(h_inicio);
                    obj.horainicio = TimeSpan.Parse(h_fin);
                    obj.estado = estado;
                    BD.SaveChanges();
                }
            }

            return Json(error, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetHorario(int id)
        {
            horario obj = BD.horario.Single(o => o.id == id);
            var horario = new { nombre = obj.nombre, h_inicio = CambiarHora(obj.horainicio.ToString()), h_fin = CambiarHora(obj.horafin.ToString()), estado = obj.estado };
            return Json(horario, JsonRequestBehavior.AllowGet);
        }
        public string CambiarHora(string hora)
        {
            string h = "";
            string[] split = hora.Split(new Char[] {':'});

            for (int i = 0; i < split.Length-1; i++)
            {
                h += split[i];
                if (i == 0)
                    h += ":";
            }
            return h;
        }
        public ActionResult DeleteHorario(int id)
        {
            horario obj = BD.horario.Single(o => o.id == id);
            BD.horario.Remove(obj);
            BD.SaveChanges();
            return Json(null, JsonRequestBehavior.AllowGet);
        }
    }
}