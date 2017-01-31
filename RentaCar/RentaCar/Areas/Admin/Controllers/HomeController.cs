using RentaCar.Areas.Admin.Models;
using RentaCar.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RentaCar.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        // GET: Admin/Home

        public RentaCarEntities db = new RentaCarEntities();
        public ActionResult Index()
        {
            var model = new Estadisticas()
            {
                Ordenes = db.Orden.Count(),
                Seguros = db.Seguro.Count(),
                Usuarios = db.AspNetUsers.Count(),
                Vehiculos = db.Vehiculo.Count()
            };

            return View(model);
        }
    }
}