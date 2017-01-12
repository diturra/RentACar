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
        public ActionResult Buscar(BuscadorPrincipal model)
        {
            RentaCarEntities db = new RentaCarEntities();

            if (!string.IsNullOrEmpty(model.Fechadesde) || !string.IsNullOrEmpty(model.Fechahasta))
            {

            }
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}