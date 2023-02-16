using Newtonsoft.Json;
const string BASE_URL = "https://icanhazdadjoke.com/";


Console.WriteLine("===========================");
Console.WriteLine("        Dad Jokes          ");
Console.WriteLine("===========================");
IDadJokeAPI dadJoke = new HazDad();

//prompt the user for inputs
string choice = "";
while(true)
{
    Console.WriteLine("Menu\n=========");
    Console.WriteLine("1. Show a random joke\n2. Search for a specific joke");
    Console.Write("Choice:");

    //show the joke based on the choice that the user made
    switch(Console.ReadLine())
    {
        case "1":
            await dadJoke.ShowSingleJoke();
        break;

        case "2":
            string keyword = "";
            while(true)
            {
                Console.Write("Keyword:");
                keyword = Console.ReadLine();
                if(!string.IsNullOrEmpty(keyword))
                    break;

                ShowError("Please enter a valid keyword!"); 
            }    

            Console.Write("Enter the number of jokes to show:(default to 10)");
            int limit = int.Parse(Console.ReadLine());  

            await dadJoke.ShowMultipleJokes(keyword, limit);    
        break;

        default:
            ShowError("Invalid choice! Please select 1 or 2");
            break;
        
    } 

    Console.WriteLine();
}



void ShowError(string error)
{
     Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(error);
    Console.ResetColor();
}
