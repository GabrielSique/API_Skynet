namespace Core.Services
{
    internal class UtilService
    {
        public static string GetID => Guid.NewGuid().ToString().Replace("-", "")[..32];
    }
}
