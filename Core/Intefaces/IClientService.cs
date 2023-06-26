using Core.Models.Entities;
using Core.Models;

namespace Core.Intefaces
{
    public interface IClientService
    {
        Task<Response<string>> AddClient(Client client);
        Task<Response<List<Client>>> GetAllClientsByFilter(string filter);
        Task<Response<string>> RemoveClient(int ID_CLIENT, string STATUS_CLIENT);
        Task<Response<List<Client>>> GetAllClients();
        Task<Response<string>> UpdateClient(Client client);
    }
}
