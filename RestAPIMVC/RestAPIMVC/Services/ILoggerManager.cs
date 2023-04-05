namespace RestAPIMVC.Services;

public interface ILoggerManager
{
    public void Error(string message);
    public void Info(string message);
    public void Warning(string message);
    public void Debug(string message);
}