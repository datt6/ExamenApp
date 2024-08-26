using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AplicacionWebNomina.Models
{
    public class DetalleOrdenCompra
    {
        [Key]
        public int DetalleOrdenCompraId { get; set; }

        public string NombreProducto { get; set; }

        [Required]
        [ForeignKey("OrdenCompra")]
        public int OrdenCompraId { get; set; }

        [Required]
        [ForeignKey("Producto")]
        public int CodigoProducto { get; set; }

        public decimal Precio { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser al menos 1.")]
        public int Cantidad { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "El descuento debe ser positivo.")]
        public decimal Descuento { get; set; }

        // Navegación a otras entidades
        public virtual OrdenCompra OrdenCompra { get; set; }
        public virtual Product Producto { get; set; }
    }
}