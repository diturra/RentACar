using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentaCar.Models
{
    public class BuscadorPrincipal
    {
        public string Fechadesde { get; set; }
        public string Timedesde { get; set; }
        public string Fechahasta { get; set; }
        public string Timehasta { get; set; }

        public string Transmision { get; set; }
        public bool AC { get; set; }
        public int Pasajeros { get; set; }
    }
}