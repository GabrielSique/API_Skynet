namespace Core.Models.Configuration
{

    public class ConfigurationDB
    {
        public BDModel? DB_SKYNET { get; set; }
    }

    public class BDModel
    {
        public string? Server { get; set; }
        public string? Name { get; set; }
        public string? User { get; set; }
        public string? Password { get; set; }
        public int Timeout { get; set; }
    }
    
}
