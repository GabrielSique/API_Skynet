using Core.Models.HttpService;

namespace Core.Intefaces
{
    public interface IHttpService
    {
        Task<HttpServiceResponse<T>> POST<T>(HttpServiceRequest request);
    }
}
