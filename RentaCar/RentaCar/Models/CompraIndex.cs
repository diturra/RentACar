﻿using RentaCar.Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentaCar.Models
{
    public class CompraIndex
    {
        public BuscadorPrincipal buscador { get; set; }
        public Vehiculo Vehiculo { get; set; }

        public List<Seguro> Seguros { get; set; }

        public List<Comuna> Comunas { get; set; }
    }
}