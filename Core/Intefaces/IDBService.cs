using Core.Models;

namespace Core.Intefaces
{
    public interface IDBService
    {
        Task<ResponseDB> CallSP(string cs, string sp, Dictionary<string, dynamic> parameters);
        Task<ResponseDB> CallSPData(string cs, string sp, Dictionary<string, dynamic> parameters);
    }
}
