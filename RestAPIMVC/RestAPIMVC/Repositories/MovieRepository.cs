using Microsoft.EntityFrameworkCore;
using RestAPIMVC.Databases;
using RestAPIMVC.Models;

namespace RestAPIMVC.Repositories;

public class MovieRepository: IMovieRepository
{
    public DbSet<Movie> movies;
    public MySqlDatabase context;

    public MovieRepository(MySqlDatabase context)
    {
        this.context = context;
        this.movies = context.Movies;
    }
    
    public async Task<IEnumerable<Movie>> All()
    {
        return await this.movies.ToArrayAsync();
    }

    public async Task<Movie> Create(Movie movie)
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

    public async Task<Movie> Get(int id)
    {
        return await this.movies.FirstOrDefaultAsync<Movie>(movie => movie.Id == id);
    }

    public async void Delete(int id)
    {
        Movie m = new Movie() {Id = id};
        this.movies.Remove(m);
        await this.context.SaveChangesAsync();

    }

    public async Task<Movie> Update(Movie movie)
    {
        Movie data = this.movies.Where(m => m.Id == movie.Id).FirstOrDefault<Movie>();
        data.Rank = movie.Rank;
        data.Rating = movie.Rating;
        data.Title = movie.Title;

        await this.context.SaveChangesAsync();

        return movie;

    }
}