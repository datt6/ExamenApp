using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AplicacionWebNomina.Models
{
    public class User
    {
        [Key]
        public int UsuarioId { get; set; }

        public string Usuario { get; set; }

        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        [StringLength(50)]
        public string Username { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [DataType(DataType.Password)]
        public string Pass { get; set; }

        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
        public string CorreoElectronico { get; set; }

        public string Rol { get; set; }

        public int StoreId { get; set; }

        public bool Activo { get; set; }

        // Otros campos opcionales
        public string Nombre { get; set; }
        public string Apellido { get; set; }

        public int StaffId { get; set; }
        public virtual Staff Staff { get; set; }
    }
}