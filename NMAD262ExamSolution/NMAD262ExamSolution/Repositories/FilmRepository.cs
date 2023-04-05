using Microsoft.EntityFrameworkCore;
using NMAD262ExamSolution.Databases;
using NMAD262ExamSolution.Models;

namespace NMAD262ExamSolution.Repositories;

public class FilmRepository : IFilmRepository
{
    private readonly MySqlDatabase _database;

    public FilmRepository(MySqlDatabase database)
    {
        this._database = database;
    }
    public async Task<Film> Create(Film film)
    {
        this._database.Films.Add(film);
        await this._database.SaveChangesAsync();
        return film;
    }

    public async Task<IEnumerable<Film>> All()
    {
        return await this._database.Films.ToListAsync();
    }

    public async Task<Film> Get(string id)
    {
        Film film = await this._database.Films.FirstOrDefaultAsync(film => film.Id == id);
        return film;
    }

    public async Task<IEnumerable<Cast>> Casts(string id)
    {
        List<Cast> casts = new List<Cast>();

        Film film = await this.Get(id);

        foreach (string name in film.Cast.Split(","))
        {
                casts.Add(new Cast{Name = name});
        }

        return casts;
    }

    public async Task<Film> Update(Film film)
    {
        _database.Films.Update(film);
        await this._database.SaveChangesAsync();
        return film;
    }

    public async Task<Rating> GetRating(string id)
    {
        Film film = await this.Get(id);
        return new Rating { Title = film.Title, Rate = film.Rating };
    }

    public async Task<IEnumerable<Film>> PartialMatchFilms(string title)
    {
        //lowering the title for easier comparing
        title = title.ToLower();

        //I use LINQ to solve this but you can also do this with tradational for loop after getting each films
        
        return await _database.Films // everything we do it working within the Films collection
            .Where(film => // "Where" implies we are searching through the collection
                film.Title.ToLower() // On each film in the collection we return inside this "Where"a toLower version of title
                    .Contains(title)) // Check to see if each toLower title contains the title passed via the parameters
            .ToListAsync(); // Take the results and make it into a list of possible matches
    }
}