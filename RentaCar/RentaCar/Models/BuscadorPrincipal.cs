using RentaCar.Datos;
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

        public int Dias { get; set; }

        public int Idvehiculo { get; set; }

        public int TotalPorDias { get; set; }
        public int AbonoPorDias { get; set; }
        public DateTime? Fechacompletadesde { get; set; }
        public DateTime? Fechacompletahasta { get; set; }

        public string Ciudad { get; set; }
        public string ComunaNombre { get; set; }
        public int Comuna { get; set; }

        public string Direccion { get; set; }

        public int TotalFinal { get; set; }
        public List<Seguro> Seguros { get; set; }

        public int totalfinal()
        {
            int total = TotalPorDias + AbonoPorDias;
            if (Seguros != null)
            {
                foreach (Seguro seg in Seguros)
                {
                    total = total + seg.valor;
                }
            }
            return total;

        }
    }
}