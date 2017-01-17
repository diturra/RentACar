using RentaCar.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentaCar.Models
{
    public class FiltroFechas
    {
        public Vehiculo Vehiculo { get; set; }
        public List<DateTime> FechasCocinadas { get; set; }

        public FiltroFechas()
        {
            FechasCocinadas = new List<DateTime>();
        }
    }

}