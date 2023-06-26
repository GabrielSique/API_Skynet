using Core.Models;
using Core.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Intefaces
{
    public interface IAuthenticatorService
    {
        //Task<Response<string>> AddUser(User user);
        //Task<Response<List<User>>> GetUsers();
        //Task<Response<string>> RemoveUser(string USER_ID);
        Task<Response<AuthenticatorDto>> Login(string EMAIL, string PASSWORD);
        //Task<Response<string>> UpdateUser(User user);
    }
}
