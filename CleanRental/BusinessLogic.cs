
using CleanRental.model;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var movies = Context.Films.ToList();
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
    }
}
