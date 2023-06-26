using Core.Models.Base;

namespace Core.Models
{
    public class Response<T> : BaseResponse
    {
        public T? Data { get; set; }
    }
}
