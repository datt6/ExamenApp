using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace AplicacionWebNomina.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public int StoreId { get; set; }

        [Required]
        public int StaffId { get; set; }

        public DateTime OrderDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public byte OrderStatus { get; set; }

        public virtual Customer Cliente { get; set; }
        public virtual Store Tienda { get; set; }
        public virtual Staff Staff { get; set; }
        public virtual ICollection<OrderItem> OrderItem { get; set; }
    }
}