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
        public ActionResult GuardarReserva(int id, string fecha, int horario, string temas, int cantidad, int tg, string usuario, bool estado)
        {
            reserva obj;

            string error = "";
            if (BD.reserva.ToList().Exists(o => o.fecha == DateTime.Parse(fecha).Date && o.horario == horario && o.usuario == id_usu(usuario)) && id == 0)
            {
                error = "Ya tienes una reserva para esta fecha y horario";
                return Json(error, JsonRequestBehavior.AllowGet);
            }
            if (BD.reserva.ToList().Exists(o => o.fecha == DateTime.Parse(fecha).Date && o.horario == horario && o.usuario == id_usu(usuario)))
            {
                error = "Ya tienes una reserva para esta fecha y horario";
                return Json(error, JsonRequestBehavior.AllowGet);
            }
            if (BD.reserva.ToList().Exists(o => o.fecha == DateTime.Parse(fecha).Date && o.horario == horario) && id == 0)
            {
                error = "proponer";
                reserva re = BD.reserva.ToList().Single(o => o.fecha == DateTime.Parse(fecha).Date && o.horario == horario);

                string tems = "";
                bool flag = false;
                foreach (var item in BD.detalle_reserva_tema.ToList().Where(o => o.reserva == re.id))
                {
                    if (flag)
                        tems += ",";
                    tems += item.tema1.nombre.ToString();
                    flag = true;
                }
                var Reserva = new
                {
                    err = error,
                    iid = re.id,
                    fech = re.fecha.ToShortDateString(),
                    hor = re.horario1.nombre,
                    tms = tems,
                    can = re.cantidad,
                    tg = re.tipo_grupo,
                    usu = re.usuario1.username,
                    est = re.estado
                };
                return Json(Reserva, JsonRequestBehavior.AllowGet);
            }

            if (string.IsNullOrEmpty(error))
            {
                if (id == 0)
                {
                    obj = new reserva();
                    obj.fecha = DateTime.Parse(fecha).Date;
                    obj.horario = horario;
                    obj.cantidad = cantidad;
                    obj.usuario = id_usu(usuario);
                    obj.tipo_grupo = tg;
                    obj.estado = estado;
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
                    obj.usuario = id_usu(usuario);
                    obj.tipo_grupo = tg;
                    obj.estado = estado;

                    foreach (var item in BD.detalle_reserva_tema.Where(o => o.reserva == id))
                    {
                        BD.detalle_reserva_tema.Remove(item);
                    }

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
            string cadena = "";
            cadena = "<table id='data' class='centered scrollable display highlight' cellspacing='0' hidden>";
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
                if (obj.usuario == id_usu(usu))
                {
                    cadena += "<a class='waves-effect waves-light btn btn-floating blue'><i class='icon-pencil-1' onclick='Editar(" + obj.id + ");'></i></a>";
                    cadena += "<a class='waves-effect waves-light btn btn-floating red'><i class='icon-trash' onclick='ModalConfirmar(" + obj.id + ");'></i></a>";
                }
                else
                {
                    cadena += "<a class='waves-effect waves-light btn btn-floating green'><i class='icon-user-plus' onclick='Sumarse(" + obj.id + "," + obj.cantidad + ");'></i></a>&nbsp;";
                }
                cadena += "</td>";
                cadena += "</tr>";
            }
            cadena += "</tbody>";
            cadena += "</table>";
            return Json(cadena, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListarReservas(string fecha)
        {
            if (fecha == "")
                fecha = DateTime.Now.Date.ToString();
            string cadena = "";
            cadena = "<table id='data' class='centered scrollable display highlight' cellspacing='0' hidden>";
            cadena += "<thead class='light-blue darken-4 white-text z-depth-3'>";
            cadena += "<tr>";
            cadena += "<th>Código</th>";
            cadena += "<th>Fecha</th>";
            cadena += "<th>Reservante</th>";
            cadena += "<th>Horario</th>";
            cadena += "<th>Tema(s)</th>";
            cadena += "<th>Nro. Per.</th>";
            cadena += "<th>Tipo de Grupo</th>";
            cadena += "<th>Estado Visita</th>";
            cadena += "<th>Visita</th>";
            cadena += "</tr>";
            cadena += "</thead>";
            cadena += "<tbody>";
            foreach (var obj in BD.reserva.ToList().Where(o => o.fecha.ToString() == DateTime.Parse(fecha).Date.ToString()))
            {
                cadena += "<tr>";
                cadena += "<td>" + obj.id + "</td>";
                cadena += "<td>" + obj.fecha.ToShortDateString() + "</td>";
                cadena += "<td>" + obj.usuario1.username + "</td>";
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


                if (obj.visita == null)
                {
                    cadena += "<td class='red-text'><i class='icon-cancel-circled'></i>&nbsp;Pendiente</td>";
                }
                else
                {
                    cadena += "<td class='green-text'><i class='icon-ok-circled'></i>&nbsp;Registrado</td>";
                }
                cadena += "<td><a class='waves-effect waves-light btn btn-floating green'><i class='icon-clipboard' onclick='Editar(" + obj.id + ");'></i></a></td>";
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
                usu = obj.usuario1.username,
                est = obj.estado
            };
            return Json(Reserva, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteReserva(int id)
        {
            try
            {
                reserva obj = BD.reserva.Single(o => o.id == id);
                BD.reserva.Remove(obj);
                BD.SaveChanges();
                return Json(null, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }

        }
        public ActionResult AdicionarReserva(int idre, string usu, int cantidad)
        {
            reserva re = BD.reserva.Single(o => o.id == idre);
            re.cantidad += cantidad;

            int idusu = BD.usuario.Single(o => o.username == usu).id;
            adicionar_reserva add_re = new adicionar_reserva();
            add_re.cantidad = cantidad;
            add_re.usuario = idusu;
            add_re.reserva = idre;
            BD.adicionar_reserva.Add(add_re);
            BD.SaveChanges();
            return Json(null, JsonRequestBehavior.AllowGet);
        }
        private int id_usu(string usuario)
        {
            int id = 0;
            if (usuario == "admin@admin")
            {
                id = 1;
            }
            else
            {
                id = BD.usuario.Single(o => o.username == usuario).id;
            }
            return id;
        }
    }
}