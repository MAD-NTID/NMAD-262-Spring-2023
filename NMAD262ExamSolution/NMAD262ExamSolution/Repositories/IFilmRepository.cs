using NMAD262ExamSolution.Models;

namespace NMAD262ExamSolution.Repositories;

public interface IFilmRepository
{
    public Task<Film> Create(Film film);
    public Task<IEnumerable<Film>> All();
    public Task<Film> Get(string id);
    public Task<IEnumerable<Cast>> Casts(string id);
    public Task<Film> Update(Film film);
    public Task<Rating> GetRating(string id);
    public Task<IEnumerable<Film>> PartialMatchFilms(string title);
}