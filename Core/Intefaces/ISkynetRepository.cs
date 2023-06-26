using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models;

namespace Core.Intefaces
{
    public interface ISkynetRepository
    {
        Task<ResponseDB> CallSP(string sp, Dictionary<string, dynamic> parameters);
        Task<ResponseDB> CallSPData(string sp, Dictionary<string, dynamic> parameters);
    }
}
