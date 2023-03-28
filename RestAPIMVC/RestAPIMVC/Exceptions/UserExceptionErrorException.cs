namespace RestAPIMVC.Exceptions;

public class UserExceptionErrorException: Exception, IUserErrorException
{

    private int status;
    private string message;

    public UserExceptionErrorException(int status, string message):base(message)
    {
        this.message = message;
        this.status = status;
    }
    public int GetStatusCode()
    {
        return this.status;
    }

    public string GetMessage()
    {
        return this.message;
    }
}