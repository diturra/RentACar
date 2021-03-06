﻿using RentaCar.Datos;
using RentaCar.Models;
using RentaCar.Models.Enum;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RentaCar.Controllers
{
    public class HomeController : Controller
    {
      private RentaCarEntities db = new RentaCarEntities();
        public ActionResult Index()
        {
            BuscadorPrincipal bp = new BuscadorPrincipal();
            if(Session["fechas"] != null)
            {
                bp = (BuscadorPrincipal)Session["fechas"];
            }
            HomeIndex hi = new HomeIndex()
            {
                ListaVehiculos = db.Vehiculo.ToList(),
                busquedaPrincipal = bp
            };
            return View(hi);
        }

        public ActionResult IndexLogin()
        {
            return View();
        }

        public ActionResult ReservaVehiculo()
        {
            BuscadorPrincipal bp = new BuscadorPrincipal();
            if (Session["fechas"] != null)
            {
                bp = (BuscadorPrincipal)Session["fechas"];
            }
            HomeReservaVehiculo Hrv = new HomeReservaVehiculo()
            {
                Comunas = db.Comuna.ToList(),
                busquedaPrincipal = bp
            };

            return View(Hrv);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpGet]
        public ActionResult Buscar(BuscadorPrincipal model)
        {
         
            ListadoVehiculo listado;
            DateTime desde;
            DateTime hasta;
            try
            {
                 desde = Convert.ToDateTime(string.Format("{0} {1}", model.Fechadesde, model.Timedesde));
                 hasta = Convert.ToDateTime(string.Format("{0} {1}", model.Fechahasta, model.Timehasta));
            }
            catch
            {
                return View("Index");
            }

            if (ModelState.IsValid)
            {
               var ordenes = db.Orden.Where(x => x.estado != (int)EstadoOrden.Cancelado).ToList();   //traigo toda las orden que no esten cancelada

                int dias = 0;
                for (DateTime date = desde; date <= hasta; date = date.AddDays(1)) // sacamos los dias 
                {
                    dias++;
                }
                model.Dias = dias;
                model.Fechacompletadesde = desde;
                model.Fechacompletahasta = hasta;

                if(!string.IsNullOrEmpty(model.Direccion))   //compruebo si hay direccion para levantar el modal.
                {
                    ViewBag.Direccion = "Si";
                }
                if(model.Comuna !=0)
                {
                    model.ComunaNombre = db.Comuna.FirstOrDefault(x => x.ID == model.Comuna).nombre;
                }

                Session["fechas"] = model;
                if (ordenes.Count() == 0) //si es 0 que no avance para que no caiga el programa
                {
                    listado = new ListadoVehiculo()
                    {
                        listadisponibles = db.Vehiculo.ToList(),
                        listaNodisponible = new List<Vehiculo>(),
                        listaCategoria = db.Categoria.ToList(),
                        Comunas = db.Comuna.ToList()
                    };
                    return View(listado);
                }
                    

                List<FiltroFechas> filtroFechas = new List<FiltroFechas>();

                List<Vehiculo> listavehiculo = db.Vehiculo.Include(x => x.Categoria).ToList();
               
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

                List<Vehiculo> listanodisponible = new List<Vehiculo>();


                foreach(FiltroFechas item in filtroFechas)
                {
                    int cont = 0;
                 
                    foreach (var fecha in item.FechasCocinadas)
                    {
                        for (DateTime date = desde; date < hasta; date = date.AddDays(1)) //colo la lista
                        {
                            if (date.ToShortDateString().Equals(fecha.Date.ToShortDateString()))
                            {
                                cont++;
                            }
                          
                        }
                      
                    }

                    if (cont != 0) 
                    {
                        listanodisponible.Add(item.Vehiculo);  //agregamos a la lista no disponible
                        listavehiculo.Remove(item.Vehiculo);  // lista de disponible lo sacamos
                    }

                }
                ViewBag.Message = "Your contact page.";
                listado = new ListadoVehiculo()
                {
                    listadisponibles = listavehiculo.OrderByDescending(x => x.valor).ToList(),
                    listaNodisponible = listanodisponible.OrderByDescending(x => x.valor).ToList(),
                    listaCategoria = db.Categoria.ToList(),
                    Comunas = db.Comuna.ToList()
                };

                return View(listado);
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

        [HttpPost]
        public ActionResult CrearOrdenModal(LugarModal lugar)
        {
            BuscadorPrincipal principal = (BuscadorPrincipal)Session["fechas"];
            lugar.Ciudad = "Santiago";
            if (ModelState.IsValid)
            {             
                principal.Direccion = lugar.Direccion;
                principal.Comuna = int.Parse(lugar.Comuna);

                principal.ComunaNombre = db.Comuna.FirstOrDefault(x => x.ID == principal.Comuna).nombre;
                Session["fechas"] = principal;

            }
            return RedirectToAction("Buscar","Home", principal);
        }
    }
}