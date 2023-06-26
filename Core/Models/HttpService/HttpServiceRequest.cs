using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.HttpService
{
    public class HttpServiceRequest
    {
        public Dictionary<string, string>? Headers { get; set; }
        public Uri? Uri { get; set; }
        public string? Method { get; set; }
        public string? Body { get; set; }
        public bool IsFormData { get; set; } = false;
    }
}
