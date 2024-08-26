using System;
using System.Collections.Generic;
using System.Linq;

using System.Web;
using System.Web.Mvc;

using System.Data;
using System.Data.SqlClient;

namespace AplicacionWebNomina.Models
{
    public class Empleado
    {
        public string Idempleado { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string correo { get; set; }
        public string genero { get; set; }
        public DateTime fechaIngreso { get; set; }
        public string clave { get; set; }
        public int id { get; set; }
        public int Codempleado { get; set; }
    }
}