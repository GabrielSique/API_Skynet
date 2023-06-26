using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos
{
    public class UsersDto
    {
        public int idUser { get; set; }
        [Required]
        public string? passwordUser { get; set; }
        [Required]
        public string? firstName { get; set; }
        [Required]
        public string? surName { get; set; }
        [Required]
        public string? secondSurName { get; set; }
        [Required]
        public string? direction { get; set; }
        [Required]
        public int phoneNumber { get; set; }
        [Required]
        public string? dni { get; set; }
        [Required]
        public string? email { get; set; }
        public string? businessName { get; set; }
        [Required]
        public int nit { get; set; }
        public string? statusUser { get; set; }
        public string? rolName { get; set; }
        public string? userName { get; set; }
        public string? statusName { get; set; }
        public int idRol { get; set; }
    }
}
