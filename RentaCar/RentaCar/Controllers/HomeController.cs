using RentaCar.Datos;
using RentaCar.Models;
using RentaCar.Models.Enum;
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
            DateTime desde = Convert.ToDateTime(model.Fechadesde);
            DateTime hasta = Convert.ToDateTime(model.Fechahasta);

            if (ModelState.IsValid)
            {
               var ordenes = db.Orden.Where(x => x.estado != (int)EstadoOrden.Cancelado).ToList();   //traigo toda las orden que no esten cancelada

                int dias = 0;
                for (DateTime date = desde; date <= hasta; date = date.AddDays(1)) // sacamos los dias 
                {
                    dias++;
                }
                model.Dias = dias;
                Session["fechas"] = model;
                if (ordenes.Count() == 0) //si es 0 que no avance para que no caiga el programa
                    return View(db.Vehiculo.ToList());


                List<FiltroFechas> filtroFechas = new List<FiltroFechas>();

                List<Vehiculo> listavehiculo = new List<Vehiculo>();
               
                foreach (var item in ordenes)
                {
                    FiltroFechas fil = new FiltroFechas();
                    fil.Vehiculo = item.Vehiculo;

                    for (DateTime date = item.desde; date < item.hasta; date = date.AddDays(1))
                    {
                         fil.FechasCocinadas.Add(date); //todas las fechas que no pueden coincidir
                    }
                    filtroFechas.Add(fil);
                }
               
                foreach(FiltroFechas item in filtroFechas)
                {
                    int cont = 0;
                 
                    foreach (var fecha in item.FechasCocinadas)
                    {
                        for (DateTime date = desde; date < hasta; date = date.AddDays(1)) //colo la lista
                        {
                            if (date == fecha)
                            {
                                cont++;
                            }
                          
                        }
                      
                    }

                    if (cont == 0)
                    {
                        listavehiculo.Add(item.Vehiculo);
                    }
                }
            
                ViewBag.Message = "Your contact page.";

                return View(listavehiculo.ToList());
            }

            return new HttpNotFoundResult();
        }
        [HttpPost]
        public ActionResult Refinar(BuscadorPrincipal model)
        {
            RentaCarEntities db = new RentaCarEntities();
            if (Session["fechas"] == null)
            {
                Session.Add("fechas", model);
            }
          
            var lista = db.Vehiculo.ToList();
            

            return View(lista);
        }
    }
}