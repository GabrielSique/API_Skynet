using Core.Models.Dtos;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models.Entities;

namespace Core.Intefaces
{
    public interface IUsersService
    {
        Task<Response<string>> AddUser(Users user);
        Task<Response<List<Users>>> GetAllUsersByFilter(string filter);
        Task<Response<string>> RemoveUser(int USER_ID, string STATUS_USER);
        Task<Response<List<Users>>> GetAllUsers();
        Task<Response<string>> UpdateUser(Users user);
        Task<Response<List<Users>>> GetAllTechnicals();
        Task<Response<List<Users>>> GetAllSupervisors();
    }
}
