namespace Core.Models.LogService
{
    public class ConfigurationLog
    {
        public string NameFile { get; set; }
        public string Path { get; set; }
        public string Date => DateTime.Now.ToString("dd-MM-yyyy");
    }
}
