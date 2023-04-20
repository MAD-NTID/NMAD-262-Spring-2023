using Microsoft.AspNetCore.Mvc;
using NMAD262ExamSolution.Repositories;

namespace NMAD262ExamSolution.Controllers;

[ApiController]
[Route("api/films")]
public class FilmController: ControllerBase
{
    private readonly IFilmRepository _repository;

    public FilmController(IFilmRepository repository)
    {
        this._repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult> Flims()
    {
        return Ok(await this._repository.All());
    }
}