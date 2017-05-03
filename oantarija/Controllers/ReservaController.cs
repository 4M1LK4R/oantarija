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
        public ActionResult GuardarReserva(int id, string fecha, int horario, string temas, int cantidad, int tg, bool vehiculo, string usuario, bool estado)
        {
            int idusu = BD.usuario.Single(o => o.username == usuario).id;
            reserva obj;

            string error = "";
            //if (BD.reserva.ToList().Exists(o => o.fecha == DateTime.Parse(fecha).Date && o.horario == horario && o.usuario == idusu && o.sala == sala) && id == 0)
            //    error = "Ya tienes una reserva para esta fehca " + fecha + " con ese es horario y esa sala";

            //if (BD.reserva.ToList().Exists(o => o.fecha == DateTime.Parse(fecha).Date && o.horario == horario && o.sala == sala))
            //{
            //    error = "proponer";
            //    reserva re = BD.reserva.ToList().Single(o => o.fecha == DateTime.Parse(fecha).Date && o.horario == horario && o.sala == sala);
            //    var rese = new { err = error, iid = re.id, fech = re.fecha.ToShortDateString(), can = re.cantidad, veh = re.vehiculo, hor = re.horario1.nombre, usu = re.usuario1.username, tem = re.tema1.nombre, dis = re.disertante1.persona.nombre, tp = re.tipo_grupo1.nombre, sal = re.sala1.nombre, salcapacidad = re.sala1.capacidad, est = re.estado };
            //    return Json(rese, JsonRequestBehavior.AllowGet);
            //}

            if (string.IsNullOrEmpty(error))
            {
                if (id == 0)
                {
                    obj = new reserva();
                    obj.fecha = DateTime.Parse(fecha).Date;
                    obj.horario = horario;
                    obj.cantidad = cantidad;
                    obj.usuario = idusu;
                    obj.tipo_grupo = tg;
                    obj.vehiculo = vehiculo;
                    obj.estado = estado;
                    obj.usuario = idusu;
                    BD.reserva.Add(obj);
                    BD.SaveChanges();
                    RegistrarDetalleReservaTema(obj.id, temas);
                }
                else
                {
                    obj = BD.reserva.Single(o => o.id == id);
                    obj.fecha = DateTime.Parse(fecha).Date;
                    obj.horario = horario;
                    obj.cantidad = cantidad;
                    obj.usuario = idusu;
                    obj.tipo_grupo = tg;
                    obj.vehiculo = vehiculo;
                    obj.estado = estado;
                    obj.usuario = idusu;
                    BD.reserva.Add(obj);
                    BD.SaveChanges();
                    RegistrarDetalleReservaTema(obj.id, temas);
                }
            }

            return Json(error, JsonRequestBehavior.AllowGet);
        }
        void RegistrarDetalleReservaTema(int idReserva, string temas)
        {
            detalle_reserva_tema obj;

            string[] split = temas.Split(new Char[] { ',' });

            for (int i = 0; i < split.Length; i++)
            {
                obj = new detalle_reserva_tema();
                obj.reserva = idReserva;
                obj.tema = int.Parse(split[i]);
                BD.detalle_reserva_tema.Add(obj);
                BD.SaveChanges();
            }
        }
        public ActionResult ListarHorarios()
        {
            string cadena = "<select id='selectHorario'>";
            cadena += "<option value='' disabled selected>Seleccionar horario</option>";
            foreach (var item in BD.horario.ToList().Where(o => o.estado))
            {
                cadena += "<option value=" + item.id + ">" + item.nombre + "</option>";
            }
            cadena += "</select>";
            return Json(cadena, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ListarTemas()
        {
            string cadena = "<select multiple id='selectTema'>";
            cadena += "<option value='' disabled selected>Seleccionar tema(s)</option>";
            foreach (var item in BD.tema.ToList().Where(o => o.estado))
            {
                cadena += "<option value=" + item.id + ">" + item.nombre + "</option>";
            }
            cadena += "</select>";
            return Json(cadena, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ListarTG()
        {
            string cadena = "<select id='selectTG'>";
            cadena += "<option value='' disabled selected>Seleccionar Tipo Grupo</option>";
            foreach (var item in BD.tipo_grupo.ToList().Where(o => o.estado))
            {
                cadena += "<option value=" + item.id + ">" + item.nombre + "</option>";
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
            cadena += "<th>Horario</th>";
            cadena += "<th>Tema(s)</th>";
            cadena += "<th>Nro. Per.</th>";
            cadena += "<th>Tipo de Grupo</th>";
            cadena += "<th>Opciones</th>";
            cadena += "</tr>";
            cadena += "</thead>";
            cadena += "<tbody>";
            foreach (var obj in BD.reserva.ToList().Where(o => o.fecha.ToString() == DateTime.Parse(fecha).Date.ToString()))
            {
                cadena += "<tr>";
                cadena += "<td>" + obj.fecha.ToShortDateString() + "</td>";
                cadena += "<td>" + obj.horario1.nombre + "</td>";
                cadena += "<td>";
                bool flag = false;
                foreach (var item in BD.detalle_reserva_tema.ToList().Where(o => o.reserva == obj.id))
                {
                    if (flag)
                        cadena += " - ";
                    cadena += item.tema1.nombre;
                    flag = true;
                }
                cadena += "</td>";
                cadena += "<td>" + obj.cantidad + "</td>";
                cadena += "<td>" + obj.tipo_grupo1.nombre + "</td>";
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

        public ActionResult GetReserva(int id)
        {
            reserva obj = BD.reserva.Single(o => o.id == id);
            string temas = "";
            bool flag = false;
            foreach (var item in BD.detalle_reserva_tema.ToList().Where(o => o.reserva == obj.id))
            {
                if (flag)
                    temas += ",";
                temas += item.tema.ToString();
                flag = true;
            }
            var Reserva = new
            {
                fech = obj.fecha.ToShortDateString(),
                hor = obj.horario,
                tms = temas,
                can = obj.cantidad,
                tg = obj.tipo_grupo,
                veh = obj.vehiculo,
                usu = obj.usuario1.username,
                est = obj.estado
            };
            return Json(Reserva, JsonRequestBehavior.AllowGet);
        }
        //public ActionResult ListarCampos()
        //{
        //    //Horario
        //    string cadena = "<p class='left-align'>Horario:</p>";
        //    cadena += "<select id='selectHorario'>";
        //    cadena += "<option value='' disabled selected>(Selecciona un horario)</option>";
        //    foreach (var obj in BD.horario.ToList().Where(o=>o.estado))
        //    {
        //        cadena += "<option value=" + obj.id + ">" + obj.nombre + "</option>";
        //    }
        //    cadena += "</select>";
        //    //Tema
        //    cadena += "<p class='left-align'>Tema:</p>";
        //    cadena += "<select id='selectTema'>";
        //    cadena += "<option value='' disabled selected>(Selecciona un tema)</option>";
        //    foreach (var obj in BD.tema.ToList().Where(o => o.estado))
        //    {
        //        cadena += "<option value=" + obj.id + ">" + obj.nombre + "</option>";
        //    }
        //    cadena += "</select>";
        //    //Disertante
        //    cadena += "<p class='left-align'>Disertante:</p>";
        //    cadena += "<select id='selectDisertante'>";
        //    cadena += "<option value='' disabled selected>(Selecciona un Disertante)</option>";
        //    foreach (var obj in BD.disertante.ToList().Where(o => o.persona.estado))
        //    {
        //        cadena += "<option value=" + obj.id + ">" + obj.persona.nombre + "</option>";
        //    }
        //    cadena += "</select>";
        //    //Tipo de Grupo
        //    cadena += "<p class='left-align'>Tipo de Grupo:</p>";
        //    cadena += "<select id='selectTipoGrupo'>";
        //    cadena += "<option value='' disabled selected>(Selecciona un Tipo de Grupo)</option>";
        //    foreach (var obj in BD.tipo_grupo.ToList().Where(o => o.estado))
        //    {
        //        cadena += "<option value=" + obj.id + ">" + obj.nombre + "</option>";
        //    }
        //    cadena += "</select>";
        //    //Sala
        //    cadena += "<p class='left-align'>Sala:</p>";
        //    cadena += "<select id='selectSala'>";
        //    cadena += "<option value='' disabled selected>(Selecciona una Sala)</option>";
        //    foreach (var obj in BD.sala.ToList().Where(o => o.estado))
        //    {
        //        cadena += "<option value=" + obj.id + ">" + obj.nombre + "</option>";
        //    }
        //    cadena += "</select>";
        //    return Json(cadena, JsonRequestBehavior.AllowGet);
        //}



        //public ActionResult DeleteReserva(int id)
        //{
        //    reserva obj = BD.reserva.Single(o => o.id == id);
        //    BD.reserva.Remove(obj);
        //    BD.SaveChanges();
        //    return Json(null, JsonRequestBehavior.AllowGet);
        //}
        //public ActionResult AdicionarReserva(int id, int cantidad)
        //{
        //    reserva obj = BD.reserva.Single(o => o.id == id);
        //    obj.cantidad = obj.cantidad + cantidad;
        //    BD.SaveChanges();
        //    return Json(null, JsonRequestBehavior.AllowGet);
        //}
    }
}