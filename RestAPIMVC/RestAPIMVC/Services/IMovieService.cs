using RestAPIMVC.Models;
using RestAPIMVC.Repositories;

namespace RestAPIMVC.Services;

public interface IMovieService
{
    
    Task<IEnumerable<MovieDetail>> AllMovieDetail();
}