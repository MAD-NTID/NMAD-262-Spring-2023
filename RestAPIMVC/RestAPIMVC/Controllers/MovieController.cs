using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestAPIMVC.Databases;
using RestAPIMVC.Models;
using RestAPIMVC.Repositories;
using RestAPIMVC.Services;

namespace RestAPIMVC.Controllers;

[Authorize]
[ApiController]
[Route("api/movies")]
public class MovieController : ControllerBase
{
    // private DbSet<Movie> movies;
    // private MySqlDatabase context;
    private IMovieService _service;

    public MovieController(IMovieService service)
    {
        this._service = service;
    }


    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult> GetMovies()
    {
        return Ok(await this._service.All());
    }

    [HttpPost]
    public async Task<ActionResult> AddMovie(Movie movie)
    {
        return Ok(await this._service.Create(movie));

    }

    [HttpGet("Details")]
    public async Task<ActionResult> Details()
    {
        return Ok(await this._service.AllMovieDetail());
    }
    
    [AllowAnonymous]
    [HttpGet("{id}")]
    public async Task<ActionResult> GetMovie(int id)
    {
        return Ok(await this._service.Get(id));
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