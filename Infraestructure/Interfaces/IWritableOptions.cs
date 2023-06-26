using Microsoft.Extensions.Options;

namespace Infraestructure.Interfaces
{
    public interface IWritableOptions<out T> : IOptions<T> where T : class, new()
    {
        void Update(Action<T> applyChanges);
    }
}
