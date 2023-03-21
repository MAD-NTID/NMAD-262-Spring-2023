using RestAPIMVC.Models;
using RestAPIMVC.Repositories;

namespace RestAPIMVC.Services;

public class MovieService : IMovieService
{
    private IMovieRepository _movieRepository;
    
    public IMovieService(IMovieRepository movieRepository)
    {
        this._movieRepository = movieRepository;
    }

    public async Task<IEnumerable<MovieDetail>> AllMovieDetail()
    {
        await this._movieRepository.QuerableMovies().Join()
    }
}