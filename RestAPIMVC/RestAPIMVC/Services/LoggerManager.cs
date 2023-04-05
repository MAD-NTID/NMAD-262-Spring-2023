namespace RestAPIMVC.Services;
using NLog;

public class LoggerManager: ILoggerManager
{
    private ILogger logger;

    public LoggerManager()
    {
        this.logger = LogManager.GetCurrentClassLogger();
    }
    
    public void Error(string message)
    {
        this.logger.Error(message);
    }

    public void Info(string message)
    {
       this.logger.Info(message);
    }

    public void Warning(string message)
    {
        this.logger.Warn(message);
    }

    public void Debug(string message)
    {
        this.logger.Debug(message);
    }
}