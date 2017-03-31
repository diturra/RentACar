using RentaCar.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentaCar.Models
{
    public class ListadoVehiculo
    {
        public List<Vehiculo> listadisponibles { get; set; }
        public List<Vehiculo> listaNodisponible { get; set; }

        public List<Categoria> listaCategoria { get; set; }

        public List<Comuna> Comunas { get; set; }
    }
}