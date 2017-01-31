using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentaCar.Areas.Admin.Models
{
    public class Estadisticas
    {
        public int Usuarios { get; set; }
        public int Ordenes { get; set; }
        public int Seguros { get; set; }
        public int Vehiculos { get; set; }
    }
}