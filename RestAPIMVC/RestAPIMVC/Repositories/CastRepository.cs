using Microsoft.EntityFrameworkCore;
using RestAPIMVC.Databases;
using RestAPIMVC.Models;

namespace RestAPIMVC.Repositories;

public class CastRepository: ICastRepository
{
    private DbSet<Cast> _casts;

    public CastRepository(MySqlDatabase context)
    {
        this._casts = context.Casts;
    }
    public IQueryable<Cast> List()
    {
        return this._casts;
    }
}