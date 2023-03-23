using RestAPIMVC.Models;
using RestAPIMVC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace RestAPIMVC.Services;

public class MovieService : IMovieService
{
    private IMovieRepository _movieRepository;
    private IActorRepository _actorRepository;
    private ICastRepository _castRepository;
    
    public MovieService(IMovieRepository movieRepository, IActorRepository actorRepository, ICastRepository castRepository)
    {
        this._movieRepository = movieRepository;
        this._actorRepository = actorRepository;
        this._castRepository = castRepository;
    }

    public async Task<IEnumerable<Movie>> All()
    {
        return await this._movieRepository.All();
    }

    public async Task<Movie> Create(Movie movie)
    {
       //some logics

       return await this._movieRepository.Create(movie);
    }

    public async Task<Movie> Get(int id)
    {
        //some logics
        return await this._movieRepository.Get(id);
    }

    public async void Delete(int id)
    {
       // some logic
       this._movieRepository.Delete(id);
    }

    public async Task<Movie> Update(Movie movie)
    {
        //some logic
        return await this._movieRepository.Update(movie);
    }

    public async Task<IEnumerable<MovieDetail>> AllMovieDetail()
    {
        

        var query = (from m in this._movieRepository.List()
                join c in this._castRepository.List() on m.Id equals c.MovieId
                join a in this._actorRepository.List() on c.ActorId equals a.Id
        
                select new MovieDetail()
                {
                    Title = m.Title,
                    Rank = m.Rank,
                    Rating = m.Rating,
                    ActorName = a.Name
                }
            );

        return query;
    }
}