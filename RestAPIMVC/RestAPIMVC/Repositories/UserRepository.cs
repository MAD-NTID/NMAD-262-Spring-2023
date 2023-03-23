using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using RestAPIMVC.Databases;
using RestAPIMVC.Models;

namespace RestAPIMVC.Repositories;

public class UserRepository: IUserRepository
{
    private DbSet<User> _users;

    public UserRepository(MySqlDatabase context)
    {
        this._users = context.Users;
    }
    public bool Authenticate(string username, string password)
    {
        password = this.Hash(password);

        User user = this._users.FirstOrDefault(user => user.Username == username && user.Password == password);

        return user != null;

        // if (user is null)
        //     return false;
        // return true;
    }

    private string Hash(string password)
    {
        SHA256 sha256 = SHA256.Create();
        byte[] passwordBytes = Encoding.ASCII.GetBytes(password);
        byte[] passwordHashedBytes = sha256.ComputeHash(passwordBytes);

        return BitConverter.ToString(passwordHashedBytes).ToLower().Replace("-", "");
    }
}