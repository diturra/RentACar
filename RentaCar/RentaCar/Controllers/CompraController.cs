using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentaCar.Datos;
using RentaCar.Models;

namespace RentaCar.Controllers
{
    public class CompraController : Controller
    {
        // GET: Compra
        private RentaCarEntities db = new RentaCarEntities();

        public ActionResult Index(int? id)
        {
            if (!id.HasValue)
                return View("Index", "Home");


            var currentUserId = User.Identity.GetUserId();
            // Unauthorized access
            if (string.IsNullOrEmpty(currentUserId))
                return new HttpUnauthorizedResult();

            var vehiculo = db.Vehiculo.FirstOrDefault(x => x.id == id);

            if(Session["fechas"] == null)
            {
                return View("Index", "Home");
            }


            BuscadorPrincipal bus = (BuscadorPrincipal)Session["fechas"];
            bus.TotalPorDias = bus.Dias * vehiculo.valor;


            CompraIndex ci = new CompraIndex()
            {
                buscador = (BuscadorPrincipal)Session["fechas"],
                Vehiculo = vehiculo
            };

            return View(ci);
        }
    }
}