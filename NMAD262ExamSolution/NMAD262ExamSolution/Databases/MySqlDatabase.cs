using Microsoft.EntityFrameworkCore;
using NMAD262ExamSolution.Models;

namespace NMAD262ExamSolution.Databases;

public class MySqlDatabase: DbContext
{
    public MySqlDatabase(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<Film> Films { get; set; }
}