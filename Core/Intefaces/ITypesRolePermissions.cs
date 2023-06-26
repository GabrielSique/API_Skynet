using Core.Models.Entities;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Intefaces
{
    public interface ITypesRolePermissions
    {
        Task<Response<List<TypesRolPermissions>>> GetAllTypesRoles();
    }
}
