using CleanRental.model;
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
                Console.WriteLine("13. Exit");
                Console.Write("Choose an option: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        DisplayAllMovies();
                        break;
                    case "2":
                        DisplayAllCommedyMovies();
                        break;
                    case "3":
                        DisplayAllCommedyActors();
                        break;
                    case "4":
                        DisplayStoreNumberByCountry();
                        break;
                    case "5":
                        DisplayMoviesRentalNumber();
                        break;
                    case "6":
                        DisplayActorsOrderedByRentalNumber();
                        break;
                    case "7":
                        DisplayMoviesOrderedByRentalIncome();
                        break;
                    case "8":
                        DisplayMoviesGenre();
                        break;
                    case "9":
                        DisplayAllMoviesByActor();
                        break;
                    case "10":
                        DisplayAllActors();
                        break;
                    case "11":
                        DisplayAllCategories();
                        break;
                    case "12":
                        DisplayAllMoviesWithActors();
                        break;
                    case "13":
                        Console.WriteLine("Exiting the application. Goodbye!");
                        return;
                    default:
                        Console.WriteLine("Invalid choice, please try again.");
                        break;
                }
            }
        }

        private void DisplayAllMoviesWithActors()
        {
            var movies = Logic.GetAllMovies();
            var actors = Logic.GetAllActors();
            
            foreach (var movie in movies)
            {
                Console.WriteLine($"{movie.FilmId} - {movie.Title}");
                var actorIds = movie.FilmActors.Select(fa => fa.ActorId).ToList();
                //foreach (var actorId in actorIds)
                //{
                //    Console.WriteLine($"  Actor ID: {actorId}");
                //}
                var movieActors = actors.Where(a => actorIds.Contains(a.ActorId)).ToList();
                foreach (var actor in movieActors)
                {
                    Console.WriteLine($"  Actor: {actor.FirstName} {actor.LastName} (ID: {actor.ActorId})");
                }
            }
        }

        private void DisplayMoviesGenre()
        {
            DisplayAllCategories();
            Console.Write("Enter genre ID to see their movies: ");
            var choice = Console.ReadLine();
            var categoryId = int.TryParse(choice, out var id) ? id : -1;
            var movies = Logic.GetMoviesByCategoryId(categoryId);
            foreach (var movie in movies)
            {
                Console.WriteLine($"{movie.FilmId} - {movie.Title}");
            }
        }

        private void DisplayMoviesOrderedByRentalIncome()
        {
            var moviesByRentalIncome = Logic.GetMoviesOrderedByRentalIncome();
            foreach (var movie in moviesByRentalIncome)
            {
                Console.WriteLine($"{movie.Item1.FilmId}, {movie.Item1.Title}, {movie.Item2}");
            }

        }



        private void DisplayActorsOrderedByRentalNumber()
        {
            var actorsByRentalNumber = Logic.GetActorsOrderedByRentalNumber();
            foreach (var actor in actorsByRentalNumber)
            {
                Console.WriteLine($"{actor.Item1} - {actor.Item2} rentals");
            }
        }

        private void DisplayMoviesRentalNumber()
        {
            var movieRentals = Logic.GetMoviesRentalNumber();
            foreach (var movieRental in movieRentals)
            {
                Console.WriteLine($"{movieRental.Item1} - {movieRental.Item2} rentals");
            }

        }

        private void DisplayStoreNumberByCountry()
        {
            var countryStores = Logic.GetStoreNumberByCountry();
            foreach (var countryStore in countryStores)
            {
                Console.WriteLine($"{countryStore.country} - {countryStore.storeNumber} stores");
            }
        }

        private void DisplayAllCommedyActors()
        {
            var actors = Logic.GetAllCommedyActors();
            foreach (var actor in actors)
            {
                Console.WriteLine($"{actor.ActorId} - {actor.FirstName} {actor.LastName}");
            }
        }

        private void DisplayAllCommedyMovies()
        {
            var movies = Logic.GetAllCommedyMovies();
            foreach (var movie in movies)
            {
                Console.WriteLine($"{movie.FilmId} - {movie.Title}");
            }
        }

        private void DisplayAllCategories()
        {
            var categories = Logic.GetAllCategories();
            foreach (var category in categories)
            {
                Console.WriteLine($"{category.CategoryId} - {category.Name}");
            }
        }

        private void DisplayAllMoviesByActor()
        {
            DisplayAllActors();
            Console.Write("Enter Actor ID to see their movies: ");
            var choice = Console.ReadLine();
            var actorId = int.TryParse(choice, out var id) ? id : -1;
            var movies = Logic.GetMoviesByActorId(actorId);
            foreach (var movie in movies)
            {
                Console.WriteLine($"{movie.FilmId} - {movie.Title}");
            }
        }

        private void DisplayAllActors()
        {
            var actors = Logic.GetAllActors();
            foreach (var actor in actors)
            {
                Console.WriteLine($"{actor.ActorId} - {actor.FirstName} {actor.LastName}");
            }
        }

        private void DisplayAllMovies()
        {
            var movies = Logic.GetAllMovies();
            foreach (var movie in movies) {
                Console.WriteLine($"{movie.FilmId} - {movie.Title}");
            }
        }
    }
}
