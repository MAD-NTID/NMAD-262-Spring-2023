using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestAPIMVC.Databases;
using RestAPIMVC.Models;
using RestAPIMVC.Repositories;

namespace RestAPIMVC.Controllers;

[ApiController]
[Route("api/movies")]
public class MovieController: ControllerBase
{
    // private DbSet<Movie> movies;
    // private MySqlDatabase context;
    private IMovieRepository _repository;

    public MovieController(IMovieRepository repository)
    {
        this._repository = repository;
    }
    

    [HttpGet]
    public async Task<ActionResult> GetMovies()
    {
        return Ok(await this._repository.All());
    }

    [HttpPost]
    public async Task<ActionResult> AddMovie(Movie movie)
    {
        return Ok(await this._repository.Create(movie));

    }

    // [HttpGet("{id}")]
    // public Movie GetMovieById(int id)
    // {
    //     foreach (Movie movie in movies)
    //     {
    //         if (movie.Id == id)
    //             return movie;
    //     }
    //
    //     return null;
    // }
    //
    // [HttpPost]
    // public ActionResult<Movie> AddMovie(Movie movie)
    // {
    //     movie.Id = nextId;
    //     this.movies.Add(movie);
    //     nextId++;
    //     return movie;
    //
    // }
    //
    // [HttpPut]
    // public ActionResult<Movie> UpdateMovie(Movie movieToUpdate)
    // {
    //     foreach (Movie movie in movies)
    //     {
    //         if (movie.Id == movieToUpdate.Id)
    //         {
    //             movie.Rank = movieToUpdate.Rank;
    //             movie.Rating = movieToUpdate.Rating;
    //             movie.Title = movieToUpdate.Title;
    //             return movie;
    //         }
    //     }
    //
    //     return NotFound($"No movie was found with the id {movieToUpdate.Id}");
    // }


}