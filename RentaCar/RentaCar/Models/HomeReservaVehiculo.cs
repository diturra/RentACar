using RentaCar.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentaCar.Models
{
    public class HomeReservaVehiculo
    {
        public List<Comuna> Comunas { get; set; }
        public BuscadorPrincipal busquedaPrincipal { get; set; }
    }
}