using Core.Enumerations;

namespace Core.Intefaces
{
    public interface ILogService
    {
        void SaveLogApp(string message, LogType logType);
    }
}
