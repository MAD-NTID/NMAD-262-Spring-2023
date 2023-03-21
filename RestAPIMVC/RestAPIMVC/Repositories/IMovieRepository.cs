using Microsoft.EntityFrameworkCore;
using RestAPIMVC.Models;

namespace RestAPIMVC.Repositories;

public interface IMovieRepository
{
    Task<IEnumerable<MovieDetail>> AllMovieDetail();
    Task<IEnumerable<Movie>> All();
    Task<Movie> Create(Movie movie);
    Task<Movie> Get(int id);
    void Delete(int id);
    Task<Movie> Update(Movie movie);

    DbSet<Movie> QuerableMovies();

}