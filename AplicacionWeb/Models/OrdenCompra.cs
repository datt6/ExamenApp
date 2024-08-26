using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Linq;
using System.Web;

namespace AplicacionWebNomina.Models
{
    public class OrdenCompra
    {
        [Key]
        public int OrdenCompraId { get; set; }

        [Required]
        [Display(Name = "Fecha de Orden")]
        public DateTime FechaOrden { get; set; }

        [Required]
        [Display(Name = "Cliente")]
        public int ClienteId { get; set; }

        [Required]
        [Display(Name = "Tienda")]
        public int TiendaId { get; set; }

        [Required]
        [Display(Name = "Empleado")]
        public int EmpleadoId { get; set; }

        public List<DetalleOrdenCompra> DetalleOrden { get; set; } = new List<DetalleOrdenCompra>();

        public IEnumerable<SelectListItem> Clientes { get; set; }
        public IEnumerable<SelectListItem> Tiendas { get; set; }
        public IEnumerable<SelectListItem> Empleados { get; set; }

        [Required]
        public decimal MontoTotal { get; set; }

        // Navegación a otras entidades
        public virtual Customer Cliente { get; set; }
        public virtual Store Tienda { get; set; }
        public virtual Empleado Empleado { get; set; }

        public virtual ICollection<DetalleOrdenCompra> Detalles { get; set; }
    }
}