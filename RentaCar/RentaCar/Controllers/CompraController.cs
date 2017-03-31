using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentaCar.Datos;
using RentaCar.Models;
using System.Threading.Tasks;
using RentaCar.Models.Enum;

namespace RentaCar.Controllers
{
    public class CompraController : Controller
    {
        // GET: Compra
        private RentaCarEntities db = new RentaCarEntities();

        public ActionResult Index(int? id)
        {
            if (!id.HasValue)
                return RedirectToAction("Index", "Home");


            var currentUserId = User.Identity.GetUserId();

            // Unauthorized access
            if (string.IsNullOrEmpty(currentUserId))
                return new HttpUnauthorizedResult();

            var vehiculo = db.Vehiculo.FirstOrDefault(x => x.id == id);

            if(Session["fechas"] == null)
                return RedirectToAction("Index", "Home");



            BuscadorPrincipal bus = (BuscadorPrincipal)Session["fechas"];
            bus.TotalPorDias = bus.Dias * vehiculo.valor;
            bus.AbonoPorDias = bus.Dias * vehiculo.abono;
            bus.Idvehiculo = id.Value;
            bus.TotalFinal = bus.totalfinal();
            bus.Seguros = new List<Seguro>();

            CompraIndex ci = new CompraIndex()
            {
                buscador = (BuscadorPrincipal)Session["fechas"],
                Vehiculo = vehiculo,
                Seguros = db.Seguro.ToList(),
                Comunas = db.Comuna.ToList()
            };

            return View(ci);
        }

        public ActionResult SeleccionPago()
        {
            var currentUserId = User.Identity.GetUserId();

            // Unauthorized access
            if (string.IsNullOrEmpty(currentUserId))
                return new HttpUnauthorizedResult();

            if (Session["fechas"] == null)
                return RedirectToAction("Index", "Home");

            BuscadorPrincipal buscador = (BuscadorPrincipal)Session["fechas"];

            Orden orden = new Orden();
            orden.userID = currentUserId;
            orden.comuna = buscador.Comuna;
            orden.desde = buscador.Fechacompletadesde.Value.Date;
            orden.hasta = buscador.Fechacompletahasta.Value.Date;
            orden.dias_totales = buscador.Dias;
            orden.direccion = buscador.Direccion;
            orden.estado = (int)EstadoOrden.Creado;
            orden.fecha_desde = buscador.Fechacompletadesde.Value;
            orden.fecha_hasta = buscador.Fechacompletahasta.Value;
            orden.tiempo_desde = buscador.Timedesde;
            orden.tiempo_hasta = buscador.Timehasta;
            orden.total_final = buscador.TotalFinal;
            orden.total_precio_abono = buscador.AbonoPorDias;
            orden.vehiculoID = buscador.Idvehiculo;
            orden.total_precio_dias = buscador.TotalPorDias;
            orden.fecha_creacion = DateTime.Now;

            db.Orden.Add(orden);
            db.SaveChanges();

            int totalSeguro = 0;
            foreach (Seguro seg in buscador.Seguros)
            {
                DetalleSeguro detseguro = new DetalleSeguro();
                detseguro.id_orden = orden.id;
                detseguro.id_seguro = seg.id;
                totalSeguro = totalSeguro + seg.valor;
                db.DetalleSeguro.Add(detseguro);
            }
         
            orden.total_precio_seguro = totalSeguro;
            db.Entry(orden).State = System.Data.Entity.EntityState.Modified;

            db.SaveChanges();

            Session.Remove("fechas");

            return View();
        }

        public JsonResult LlenandoSeguros(string idseguro)
        {
            int id = int.Parse(idseguro);
            Seguro seguro = db.Seguro.FirstOrDefault(x => x.id == id);

            BuscadorPrincipal bus = (BuscadorPrincipal)Session["fechas"];

            

            if (bus.Seguros == null)
                bus.Seguros = new List<Seguro>();

            if (bus.Seguros.FirstOrDefault(x=>x.id == seguro.id) != null)
            {
                bus.Seguros.Remove(bus.Seguros.FirstOrDefault(x => x.id == seguro.id));
                bus.TotalFinal = bus.totalfinal();
                SeguroString ss = new SeguroString()
                {
                    Nombre = seguro.nombre,
                    IdSeguro = seguro.id.ToString(),
                    Valor = string.Format("{0} {1:N0}", "$", seguro.valor),
                    ValorFinal = string.Format("{0} {1:N0}", "$", bus.TotalFinal)
                };
                return Json(ss);
            }
            else
            {
                bus.Seguros.Add(seguro);
                bus.TotalFinal = bus.totalfinal();
                SeguroString ss = new SeguroString()
                {
                    Nombre = seguro.nombre,
                    IdSeguro = seguro.id.ToString(),
                    Valor = string.Format("{0} {1:N0}", "$", seguro.valor),
                    ValorFinal = string.Format("{0} {1:N0}", "$", bus.TotalFinal)
                };
                return Json(ss);
            }
            
        }

    }
}