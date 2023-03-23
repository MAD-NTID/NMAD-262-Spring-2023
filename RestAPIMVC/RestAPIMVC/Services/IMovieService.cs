using RestAPIMVC.Models;
using RestAPIMVC.Repositories;

namespace RestAPIMVC.Services;

public interface IMovieService
{

    Task<IEnumerable<Movie>> All();
    Task<Movie> Create(Movie movie);
    Task<Movie> Get(int id);
    void Delete(int id);
    Task<Movie> Update(Movie movie);
    Task<IEnumerable<MovieDetail>> AllMovieDetail();
}