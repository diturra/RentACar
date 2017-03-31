using RentaCar.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentaCar.Models
{
    public class HomeIndex
    {
        public List<Vehiculo> ListaVehiculos { get; set; }
        public BuscadorPrincipal busquedaPrincipal { get; set; }
    }
}