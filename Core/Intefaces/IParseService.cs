using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Intefaces
{
    public interface IParseService
    {
        public T Deserealize<T>(string model);
        public string Serialize(object model);
    }
}
