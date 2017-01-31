using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RentaCar.Areas.Admin.Models
{
    public class VehiculoModelo
    {
        [Required]
        [Display(Name = "Categoria")]
        public int categoriaID { get; set; }
        [Required]
        [Display(Name = "Modelo")]
        public string modelo { get; set; }
        [Required]
        [Display(Name = "Capacidad")]
        public int capacidad { get; set; }
        [Required]
        [Display(Name = "Transmision")]
        public string transmision { get; set; }
        [Display(Name = "Aire acondicionado")]
        public bool aire_acondicionado { get; set; }
        [Display(Name = "Cierre centralizado")]
        public bool cierre_centralizado { get; set; }

        [Display(Name = "Abono")]
        [Required]
        public int abono { get; set; }
        [Required]
        [Display(Name = "Precio:")]
        public int valor { get; set; }
        [Required]
        [Display(Name = "Foto")]
        HttpPostedFileBase files { get; set; }

        [Display(Name = "Disponible?")]
        public bool disponible { get; set; }
        [Required]
        [Display(Name = "Puertas")]
        public int puertas { get; set; }
    }
}