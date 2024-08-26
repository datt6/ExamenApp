using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AplicacionWebNomina.Models
{
    public class Product
    {
        [Key]
        public int Codigo { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public decimal List_Price { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "El precio debe ser positivo.")]
        public decimal Precio { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "El stock debe ser positivo.")]
        public int Stock { get; set; }
    }
}