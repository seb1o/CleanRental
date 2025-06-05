
using CleanRental.model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CleanRental
{
    internal class BusinessLogic
    {
        public DvdRentalContext Context { get; set; }
        public BusinessLogic(DvdRentalContext context)
        {
            Context = context;
        }

        internal List<Film> GetAllMovies()
        {
            var movies = Context.Films.Include(f => f.FilmActors).ToList();
            return movies;
        }

        internal List<Actor> GetAllActors()
        {
            var actors = Context.Actors.ToList();
            return actors;
        }

        internal List<Category> GetAllCategories()
        {
            var categories = Context.Categories.ToList();
            return categories;
        }

        internal List<Film> GetMoviesByActorId(int actorId)
        {
            var movies = Context.Films
                .Where(f => f.FilmActors.Any(fa => fa.ActorId == actorId))
                .ToList();
            return movies;
        }

        internal List<Film> GetAllCommedyMovies()
        {   
            var commedyId = 5; // Assuming 5 is the ID for Comedy category

            var commedyMovies = Context.Films
                .Where(f => f.FilmCategories.Any(fc => fc.CategoryId == commedyId))
                .ToList();
            return commedyMovies;
        }

        internal List<Actor> GetAllCommedyActors()
        {
            //var commedyMovies = Context.Films
            //    .Where(f => f.FilmCategories.Any(fc => fc.Category.Name == "Comedy"))
            //    .Include(f => f.FilmActors)
            //    .ThenInclude(fa => fa.Actor)
            //    .ToList();

            //var commedyActors = commedyMovies
            //    .SelectMany(f => f.FilmActors)
            //    .Select(fa => fa.Actor)
            //    .Distinct()
            //    .OrderBy(a => a.ActorId)
            //    .ToList();

            var commedyActors =
                (from a in Context.Actors
                join fa in Context.FilmActors on a.ActorId equals fa.ActorId
                join f in Context.Films on fa.FilmId equals f.FilmId
                join fc in Context.FilmCategories on f.FilmId equals fc.FilmId
                where fc.CategoryId == 5 // Assuming 5 is the ID for Comedy category
                orderby a.ActorId
                select a)
                .Distinct()
                .ToList();

            return commedyActors;

        }

        internal List<(string country, int storeNumber)> GetStoreNumberByCountry()
        {
            var storeNumbers = Context.Stores
                                      .Include(s => s.Address)
                                      .ThenInclude(a => a.City)
                                      .ThenInclude(c => c.Country)
                                      .GroupBy(s => s.Address.City.Country.Country1)
                                      .AsEnumerable()
                                      .Select(g => (country: g.Key, storeNumber: g.Count()))
                                      .ToList();

            return storeNumbers;
        }

        internal List<Tuple<string, int>> GetMoviesRentalNumber()
        {
            var filmRentals = 
                (from f in Context.Films
                join i in Context.Inventories on f.FilmId equals i.FilmId
                join r in Context.Rentals on i.InventoryId equals r.InventoryId
                group r by f into g
                orderby g.Count() descending
                 select new Tuple <string, int> (
                    g.Key.Title,
                    g.Count()
                )).ToList();

            return filmRentals;
        }

        internal List<Tuple<string, int>> GetActorsOrderedByRentalNumber()
        {
            var actorsByRental = Context.Payments
                                        .Include(p => p.Rental)
                                        .ThenInclude(r => r.Inventory)
                                        .ThenInclude(i => i.Film)
                                        .ThenInclude(f => f.FilmActors)
                                        .ThenInclude(fa => fa.Actor)
                                        .SelectMany(p => p.Rental.Inventory.Film.FilmActors.Select(fa => fa.Actor))
                                        .GroupBy(a => new {a.ActorId, a.FirstName, a.LastName})
                                        .OrderByDescending(g => g.Count())
                                        .Select(g => new Tuple <string, int>(g.Key.FirstName + ' ' + g.Key.LastName, g.Count()))
                                        .ToList();

            return actorsByRental;
        }


        internal List<Film> GetMoviesByCategoryId(int categoryId)
        {
            var movies = Context.Films
                .Where(f => f.FilmCategories.Any(fc => fc.CategoryId == categoryId))
                .ToList();
            return movies;
        }

        internal List<Tuple<Film, decimal>> GetMoviesOrderedByRentalIncome()
        {
            var moviesOrderedByRentalIncome = Context.Films
                                                     .Include(f => f.Inventories)
                                                     .ThenInclude(i => i.Rentals)
                                                     .ThenInclude(r => r.Payments)
                                                     .Select(f => new Tuple<Film, decimal>
                                                        (
                                                         f,
                                                         f.Inventories.Sum(i =>
                                                         i.Rentals.Sum(r =>
                                                         r.Payments.Sum(p => p.Amount)))
                                                         )
                                                        )
                                                     .ToList();
            moviesOrderedByRentalIncome.Sort((t1, t2) => (int)Math.Round(t2.Item2 - t1.Item2) );
            return moviesOrderedByRentalIncome;
        }
    }
}
