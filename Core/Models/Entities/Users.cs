using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Entities
{
    public class Users
    {
        public int idUser { get; set; }
        public string? passwordUser { get; set; }
        public string? firstName { get; set; }
        public string? surName { get; set; }
        public string? secondSurName { get; set; }
        public string? direction { get; set; }   
        public int phoneNumber { get; set; }
        public string? dni { get; set; }
        public string? email { get; set; }
        public string? businessName { get; set; }
        public int nit { get; set; }
        public string? statusUser { get; set; }
        public string? rolName { get; set; }
        public string? userName { get; set; }
        public string? statusName { get; set; }
        public int idRol { get; set; }
    }
}
