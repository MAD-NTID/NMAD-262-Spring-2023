using Microsoft.EntityFrameworkCore;
using RestAPIMVC.Models;

namespace RestAPIMVC.Databases;

public class MySqlDatabase: DbContext
{
    public MySqlDatabase(DbContextOptions options) : base(options)
    {
        
    }
    
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Cast> Casts { get; set; }
    public DbSet<Actor> Actors { get; set; }
    
    public DbSet<User> Users { get; set; }
}