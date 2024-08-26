using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AplicacionWebNomina.Models
{
    public class Stock
    {
        [Key, Column(Order = 0)]
        public int StoreId { get; set; }

        [Key, Column(Order = 1)]
        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public virtual Store Store { get; set; }
        public virtual Product Product { get; set; }
    }
}