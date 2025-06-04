using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanRental
{
    internal class Tui
    {
        public BusinessLogic Logic { get; set; }
        public Tui(BusinessLogic logic)
        {
            Logic = logic;
        }

        public void Run()
        {
            while (true)
            {
                Console.WriteLine("Welcome to CleanRental!");
                Console.WriteLine("1. Show all movies");
                Console.WriteLine("2. Show all commedy movies");
                Console.WriteLine("3. Show all commedy actors");
                Console.WriteLine("4. Show store number by country");
                Console.WriteLine("5. Show movies rental number");
                Console.WriteLine("6. Show actors ordered by rental number");
                Console.WriteLine("7. Show movies ordered by rental income");
            //---------------------------------------------------------------------
                Console.WriteLine("8. Show all movies by genre");
                Console.WriteLine("9. Show all movies by actor");
                Console.WriteLine("10. Show all actors");
                Console.WriteLine("11. Show all categories");
                Console.WriteLine("12. Show all Movies with Actors");
                Console.WriteLine("2. Exit");
                Console.Write("Choose an option: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        DisplayAllMovies();
                        break;
                    case "2":
                        Console.WriteLine("Exiting the application. Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid choice, please try again.");
                        break;
                }
            }
        }

        private void DisplayAllMovies()
        {
            var movies = Logic.GetAllMovies();
            foreach (var movie in movies) {
                Console.WriteLine(movie.Title);
            }
        }
    }
}
