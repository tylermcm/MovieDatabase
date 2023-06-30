using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieDatabase.Data;
using MovieDatabase.Models;

namespace MovieDatabase.Pages.Movies
{
    public class IndexModel : PageModel
    {
        private readonly MovieDatabase.Data.MovieDatabaseContext _context;

        public IndexModel(MovieDatabase.Data.MovieDatabaseContext context)
        {
            _context = context;
        }

        public IList<Movie> Movie { get;set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public SelectList? Genres { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? MovieGenre { get; set; }

        public async Task OnGetAsync(string sortOrder)
        {
            // Use LINQ to get list of genres.
            IQueryable<string> genreQuery = from m in _context.Movie
                orderby m.Genre
                select m.Genre;

            var movies = from m in _context.Movie
                select m;
            //Code to interface with the search bar to search for a movie title in the database
            if (!string.IsNullOrEmpty(SearchString))
            {
                movies = movies.Where(s => s.MovieName.Contains(SearchString));
            }

            //Code to interface with the dropdown menu to search for a movie in the database
            if (!string.IsNullOrEmpty(MovieGenre))
            {
                movies = movies.Where(x => x.Genre == MovieGenre);
            }
            Genres = new SelectList(await genreQuery.Distinct().ToListAsync());

            //Switch case to select sorting method
            switch (sortOrder)
            {
                case "name_asc":
                    movies = movies.OrderBy(m => m.MovieName);
                    break;
                case "releaseYear":
                    movies = movies.OrderBy(m => m.ReleaseYear);
                    break;
                case "releaseYear_desc":
                    movies = movies.OrderByDescending(m => m.ReleaseYear);
                    break;
                default:
                    movies = movies.OrderBy(m => m.MovieName);
                    break;
            }
            Movie = await movies.ToListAsync();

        }
    }
}
