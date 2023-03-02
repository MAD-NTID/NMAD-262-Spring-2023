using Microsoft.EntityFrameworkCore;
using RestAPIMVC.Databases;
using RestAPIMVC.Models;

namespace RestAPIMVC.Repositories;

public class MovieRepository
{
    public DbSet<Movie> movies;
    public MySqlDatabase context;

    public MovieRepository(MySqlDatabase context)
    {
        this.context = context;
        this.movies = context.Movies;
    }

    public async Task<IEnumerable<Movie>> GetMovies()
    {
        return await this.movies.ToArrayAsync();
    }

    public async Task<Movie> AddMovie(Movie movie)
    {
        if (string.IsNullOrEmpty(movie.Title))
        {
            throw new Exception("Movie title cannot be empty");
        }
        
        if (movie.Rank < 1 || movie.Rating < 1)
        {
            throw new Exception("Movie rank or rating must be greater than 0");
        }
        await this.movies.AddAsync(movie);
        await this.context.SaveChangesAsync();

        return movie;
    }
}