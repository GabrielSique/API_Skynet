using Core.Enumerations;
using Core.Intefaces;
using Core.Models.LogService;

namespace Infraestructure.Services
{
    public class LogService : ILogService
    {
        private readonly ConfigurationLog _configurationLog;
        private readonly IConfigurationService _configurationService;
        public LogService(IConfigurationService configurationService)
        {
            _configurationService = configurationService;
            _configurationLog = _configurationService.Get<ConfigurationLog>(Configuration.LogService);
        }
        private static void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }
        public void SaveLogApp(string message, LogType logType)
        {
            string type;
            if (logType == LogType.Error)
                type = "Error";
            else
                type = "Information";
            string path = $"{_configurationLog.Path}{_configurationLog.Date}\\{type}\\";
            try
            {
                CreateDirectory(path);
                string line = $"[{DateTime.Now:dd/MM/yyyy} {DateTime.Now:HH:mm:ss fff}]";
                line = $"{line}: {message}{Environment.NewLine}";
                File.AppendAllText($"{path}{_configurationLog.NameFile}", line);
            }
            catch (Exception ex)
            {
                Console.WriteLine(nameof(LogService), ex.Message, ex.StackTrace);
            }
        }
    }
}
