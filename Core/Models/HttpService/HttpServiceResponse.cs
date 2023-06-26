using Core.Models.Base;

namespace Core.Models.HttpService
{
    public class HttpServiceResponse<T> : BaseResponse
    {
        public T? Data { get; set; }
    }
}
