using Microsoft.EntityFrameworkCore;
using RestAPIMVC.Models;
using RestAPIMVC.Databases;

namespace RestAPIMVC.Repositories;

public class ActorRepository: IActorRepository
{
    private DbSet<Actor> _actors;

    public ActorRepository(MySqlDatabase context)
    {
        this._actors = context.Actors;
    }

    public IQueryable<Actor> List()
    {
        return this._actors;
    }
}