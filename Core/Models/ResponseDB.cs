using Core.Models.Base;
using System.Data;

namespace Core.Models
{
    public class ResponseDB : BaseResponse
    {
        public DataTable? Data { get; set; }
    }
}
