using Core.Intefaces;
using Newtonsoft.Json;

namespace Infraestructure.Services
{
    internal class ParseService : IParseService
    {
        public T Deserealize<T>(string model) => JsonConvert.DeserializeObject<T>(model);
        public string Serialize(object model) => JsonConvert.SerializeObject(model);
    }
}
