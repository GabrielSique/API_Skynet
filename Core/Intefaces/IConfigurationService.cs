namespace Core.Intefaces
{
    public interface IConfigurationService
    {
        public T Get<T>(string section);
    }
}
