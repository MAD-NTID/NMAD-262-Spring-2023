using APIExampleCSharp;
using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

List<Movie> movies = new List<Movie>()
{
    new Movie { Id = 100, Rank = 1, Title="Star War", Rating = 10.0},
    new Movie { Id = 101, Rank = 1, Title = "Spiderman", Rating = 5 },
    new Movie { Id = 102, Rank = 1, Title = "Power Ranger", Rating = 4.6}
};


int nextId = 103;

app.MapPost("/api/movies", (Movie movie) =>
{
    //validate our data
    if(movie.Rank < 0)
    {
        return Results.Problem("Movie rank must be >=0");
    }

    if (string.IsNullOrEmpty(movie.Title))
        return Results.Problem("Movie title cannot be empty!");

    if (movie.Rating < 0)
        return Results.Problem("Movie rating cannot be negative!");

    //set the movie id to the one that we get from the generator
    movie.Id = nextId;
    //add the moive
    movies.Add(movie);
    nextId++; //increment the id to the next available id

    //finally inform the user that the movie was added
    return Results.Ok("The movie was successfully added");
});

app.MapDelete("/api/movies/{id}", (int id) =>
{
    foreach (Movie movie in movies)
    {
        if (movie.Id == id)
        {
            movies.Remove(movie);
            return Results.Ok("The movie was successfully removed");
        }
    }

    return Results.NotFound($"The movie with the id {id} was not found");
});

app.MapPut("/api/movies", (Movie movieToUpdate) =>
{

    foreach(Movie movie in movies)
    { 
        if(movie.Id == movieToUpdate.Id)
        { 

            movie.Rating = movieToUpdate.Rating;
            movie.Rank = movieToUpdate.Rank;
            movie.Title = movieToUpdate.Title;
            return Results.Ok("The movie was successfully updated");
        }
    }


    return Results.NotFound($"The movie with the id {movieToUpdate.Id} was not found");

});

app.MapGet("/api/movies/{id}", (int id) =>
{

    //using foreach approach
    foreach(Movie movie in movies)
    {
        if(movie.Id == id)
        {
            Console.WriteLine(movie.Id);
            return Results.Ok(movie);
        }
            
    }

    return Results.NotFound($"No movie with the id {id} was found");

    //return $"No movie was found with the id {id}";
    //link approach
    //Results.Ok(movies.FirstOrDefault(movie => movie.Id == id));

});


app.MapGet("/api/movies", () =>
{
    return movies;
})

.WithOpenApi();

app.Run();