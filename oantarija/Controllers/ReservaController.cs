using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using oantarija.Models;

namespace oantarija.Controllers
{
    public class ReservaController : Controller
    {
        oantarijaEntities BD = new oantarijaEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ListarCampos()
        {
            //Horario
            string cadena = "<p class='left-align'>Horario:</p>";
            cadena += "<select id='selectHorario'>";
            cadena += "<option value='' disabled selected>(Selecciona un horario)</option>";
            foreach (var obj in BD.horario.ToList().Where(o=>o.estado))
            {
                cadena += "<option value=" + obj.id + ">" + obj.nombre + "</option>";
            }
            cadena += "</select>";
            //Tema
            cadena += "<p class='left-align'>Tema:</p>";
            cadena += "<select id='selectTema'>";
            cadena += "<option value='' disabled selected>(Selecciona un tema)</option>";
            foreach (var obj in BD.tema.ToList().Where(o => o.estado))
            {
                cadena += "<option value=" + obj.id + ">" + obj.nombre + "</option>";
            }
            cadena += "</select>";
            //Disertante
            cadena += "<p class='left-align'>Disertante:</p>";
            cadena += "<select id='selectDisertante'>";
            cadena += "<option value='' disabled selected>(Selecciona un Disertante)</option>";
            foreach (var obj in BD.disertante.ToList().Where(o => o.persona.estado))
            {
                cadena += "<option value=" + obj.id + ">" + obj.persona.nombre + "</option>";
            }
            cadena += "</select>";
            //Tipo de Grupo
            cadena += "<p class='left-align'>Tipo de Grupo:</p>";
            cadena += "<select id='selectTipoGrupo'>";
            cadena += "<option value='' disabled selected>(Selecciona un Tipo de Grupo)</option>";
            foreach (var obj in BD.tipo_grupo.ToList().Where(o => o.estado))
            {
                cadena += "<option value=" + obj.id + ">" + obj.nombre + "</option>";
            }
            cadena += "</select>";
            //Sala
            cadena += "<p class='left-align'>Sala:</p>";
            cadena += "<select id='selectSala'>";
            cadena += "<option value='' disabled selected>(Selecciona una Sala)</option>";
            foreach (var obj in BD.sala.ToList().Where(o => o.estado))
            {
                cadena += "<option value=" + obj.id + ">" + obj.nombre + "</option>";
            }
            cadena += "</select>";
            return Json(cadena, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListarReservasFechaUsuario(string fecha, string usu)
        {
            if (fecha == "")
                fecha = DateTime.Now.Date.ToString();
            int idusu = BD.usuario.Single(o => o.username == usu).id;
            string cadena = "";
            cadena = "<table id='data' class='scrollable display highlight' cellspacing='0' hidden>";
            cadena += "<thead class='light-blue darken-4 white-text z-depth-3'>";
            cadena += "<tr>";
            cadena += "<th>Fecha</th>";
            cadena += "<th>Cantidad</th>";
            cadena += "<th>sala</th>";
            cadena += "<th>Horario</th>";
            cadena += "<th>Tipo de Grupo</th>";
            cadena += "<th>Tema</th>";
            cadena += "<th>usuario</th>";
            cadena += "<th>Opciones</th>";
            cadena += "</tr>";
            cadena += "</thead>";
            cadena += "<tbody>";
            foreach (var obj in BD.reserva.ToList().Where(o=>o.fecha.ToString() == DateTime.Parse(fecha).Date.ToString()))
            {
                string f = obj.fecha.ToShortDateString();
                cadena += "<tr>";
                cadena += "<td>" + obj.fecha.ToShortDateString() + "</td>";
                cadena += "<td>" + obj.cantidad + "</td>";
                cadena += "<td>" + obj.sala1.nombre + "</td>";
                cadena += "<td>" + obj.horario1.nombre + "</td>";
                cadena += "<td>" + obj.tipo_grupo1.nombre + "</td>";
                cadena += "<td>" + obj.tema1.nombre + "</td>";
                cadena += "<td>" + obj.usuario1.persona.nombre + " " + obj.usuario1.persona.apellido+ "</td>";
                cadena += "<td>";
                if (obj.usuario == idusu)
                {
                    cadena += "<a class='waves-effect waves-light btn btn-floating blue'><i class='icon-pencil-1' onclick='Editar(" + obj.id + ");'></i></a>&nbsp;";
                    cadena += "<a class='waves-effect waves-light btn btn-floating red'><i class='icon-trash' onclick='ModalConfirmar(" + obj.id + ",\"" + obj.fecha + "\");'></i></a>";
                }
                //else
                //{
                //    cadena += "<a class='waves-effect waves-light btn btn-floating green'><i class='icon-user-plus' onclick='Sumarse(" + obj.id + ");'></i></a>&nbsp;";
                //}
                cadena += "</td>";
                cadena += "</tr>";
            }
            cadena += "</tbody>";
            cadena += "</table>";
            return Json(cadena, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GuardarReserva(int id, string fecha, int cantidad, bool vehiculo, int horario,string usuario, int tema,int disertante,int tipogrupo,int sala, bool estado)
        {
            int idusu = BD.usuario.Single(o => o.username == usuario).id;
            reserva obj;
            
            string error = "";
            if (string.IsNullOrEmpty(cantidad.ToString()))
                error = "El campo cantidad esta vacio";           
            if (BD.reserva.ToList().Exists(o => o.fecha == DateTime.Parse(fecha).Date  && o.horario == horario && o.usuario == idusu && o.sala == sala) && id == 0)
                error = "Ya tienes una reserva para esta fehca "+fecha+" con ese es horario y esa sala";

            if (BD.reserva.ToList().Exists(o => o.fecha == DateTime.Parse(fecha).Date && o.horario == horario && o.sala == sala))
            {
                error = "proponer";
                reserva re = BD.reserva.ToList().Single(o => o.fecha == DateTime.Parse(fecha).Date && o.horario == horario && o.sala == sala);
                var rese = new {err = error, iid = re.id, fech = re.fecha.ToShortDateString(), can = re.cantidad, veh = re.vehiculo, hor = re.horario1.nombre, usu = re.usuario1.username, tem = re.tema1.nombre, dis = re.disertante1.persona.nombre, tp = re.tipo_grupo1.nombre, sal = re.sala1.nombre, salcapacidad = re.sala1.capacidad, est = re.estado };
                return Json(rese, JsonRequestBehavior.AllowGet);
            }
           
            if (string.IsNullOrEmpty(error))
            {
                if (id == 0)
                {
                    obj = new reserva();
                    obj.fecha = DateTime.Parse(fecha).Date;
                    obj.cantidad = cantidad;
                    obj.vehiculo = vehiculo;
                    obj.horario = horario;
                    obj.usuario = idusu;
                    obj.tema = tema;
                    obj.disertante = disertante;
                    obj.tipo_grupo = tipogrupo;
                    obj.sala = sala;
                    obj.estado = estado;
                    BD.reserva.Add(obj);
                    BD.SaveChanges();
                }
                else
                {
                    obj = BD.reserva.Single(o => o.id == id);
                    obj.fecha = DateTime.Parse(fecha).Date;
                    obj.cantidad = cantidad;
                    obj.vehiculo = vehiculo;
                    obj.horario = horario;
                    obj.usuario = idusu;
                    obj.tema = tema;
                    obj.disertante = disertante;
                    obj.tipo_grupo = tipogrupo;
                    obj.sala = sala;
                    obj.estado = estado;
                    BD.SaveChanges();
                }
            }

            return Json(error, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetReserva(int id)
        {
            reserva obj = BD.reserva.Single(o => o.id == id);
            var Reserva = new {fech = obj.fecha.ToShortDateString(), can = obj.cantidad, veh = obj.vehiculo , hor = obj.horario , usu = obj.usuario1.username , tem = obj.tema, dis=obj.disertante , tp = obj.tipo_grupo, sal = obj.sala, est = obj.estado};
            return Json(Reserva, JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult DeleteReserva(int id)
        {
            reserva obj = BD.reserva.Single(o => o.id == id);
            BD.reserva.Remove(obj);
            BD.SaveChanges();
            return Json(null, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AdicionarReserva(int id, int cantidad)
        {
            reserva obj = BD.reserva.Single(o => o.id == id);
            obj.cantidad = obj.cantidad + cantidad;
            BD.SaveChanges();
            return Json(null, JsonRequestBehavior.AllowGet);
        }
    }
}