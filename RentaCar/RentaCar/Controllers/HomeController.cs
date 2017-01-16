using RentaCar.Datos;
using RentaCar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RentaCar.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
          
            return View();
        }

        public ActionResult IndexLogin()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpGet]
        public ActionResult Buscar(BuscadorPrincipal model)
        {
            RentaCarEntities db = new RentaCarEntities();

            if (!string.IsNullOrEmpty(model.Fechadesde) || !string.IsNullOrEmpty(model.Fechahasta))
            {
                Session["fechas"] = model;
                //var vehiculosdisponible = db.Vehiculo.Where(x => x.disponible);
            }
            var lista = db.Vehiculo.ToList();
            ViewBag.Message = "Your contact page.";

            return View(lista);
        }
        [HttpPost]
        public ActionResult Refinar(BuscadorPrincipal model)
        {
            RentaCarEntities db = new RentaCarEntities();
            if (Session["fechas"] == null)
            {
                Session.Add("fechas", model);
            }
            else
            {
                BuscadorPrincipal sesion = (BuscadorPrincipal)Session["fechas"];
            }
            var lista = db.Vehiculo.ToList();
            

            return View(lista);
        }
    }
}