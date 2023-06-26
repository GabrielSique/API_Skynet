using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Dtos
{
    public class TypesRolPermissionsDto
    {
        public int idRol { get; set; }
        public string? rolName { get; set; }
        public char createUser { get; set; }
        public char updateUser { get; set; }
        public char readUser { get; set; }
        public char createVisit { get; set; }
        public char updateVisit { get; set; }
        public char reportClient { get; set; }
        public char reportSuper { get; set; }
        public char sendEmail { get; set; }
    }
}
