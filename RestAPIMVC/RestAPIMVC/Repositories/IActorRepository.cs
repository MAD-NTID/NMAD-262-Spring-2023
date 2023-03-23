using RestAPIMVC.Models;

namespace RestAPIMVC.Repositories;

public interface IActorRepository
{
    IQueryable<Actor> List();
}