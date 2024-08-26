using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AplicacionWebNomina.Data
{
    public class AplicacionWebNominaContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public AplicacionWebNominaContext() : base("name=AplicacionWebNominaContext")
        {
        }

        public System.Data.Entity.DbSet<AplicacionWebNomina.Models.User> Usuarios { get; set; }

        public System.Data.Entity.DbSet<AplicacionWebNomina.Models.OrdenCompra> OrdenCompras { get; set; }

        public System.Data.Entity.DbSet<AplicacionWebNomina.Models.Customer> Clientes { get; set; }

        public System.Data.Entity.DbSet<AplicacionWebNomina.Models.Empleado> Empleadoes { get; set; }

        public System.Data.Entity.DbSet<AplicacionWebNomina.Models.Store> Tiendas { get; set; }

        public System.Data.Entity.DbSet<AplicacionWebNomina.Models.Order> Orders { get; set; }

        public System.Data.Entity.DbSet<AplicacionWebNomina.Models.Staff> Staffs { get; set; }
    }
}
