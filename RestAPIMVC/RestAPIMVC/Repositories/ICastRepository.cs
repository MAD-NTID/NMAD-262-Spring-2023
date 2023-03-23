using RestAPIMVC.Models;

namespace RestAPIMVC.Repositories;

public interface ICastRepository
{
    IQueryable<Cast> List();
}