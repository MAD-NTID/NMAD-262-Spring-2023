interface IDadJokeAPI
{
    public Task ShowSingleJoke();
    public Task ShowMultipleJokes(string keyword, int limit = 10);
}