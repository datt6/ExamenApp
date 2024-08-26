using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AplicacionWebNomina.Models
{
    public class Staff
    {
        [Key]
        public int StaffId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string Phone { get; set; }
        public bool Active { get; set; }
        public int StoreId { get; set; }
        public int? ManagerId { get; set; }

        public virtual Store Store { get; set; }
        public virtual Staff Manager { get; set; }
        public virtual ICollection<Staff> Subordinates { get; set; }
    }
}