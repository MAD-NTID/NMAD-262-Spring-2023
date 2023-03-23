namespace RestAPIMVC.Repositories;

public interface IUserRepository
{
    bool Authenticate(string username, string password);
}