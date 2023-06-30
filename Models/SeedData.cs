using Microsoft.EntityFrameworkCore;
using MovieDatabase.Data;

namespace MovieDatabase.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new MovieDatabaseContext(
                   serviceProvider.GetRequiredService<
                       DbContextOptions<MovieDatabaseContext>>()))
        {
            if (context == null || context.Movie == null)
            {
                throw new ArgumentNullException("Null MovieDatabaseContext");
            }

            // Look for any movies.
            if (context.Movie.Any())
            {
                return;   // DB has been seeded
            }

            //Seed the database using the new movie objects obtained from the Movie Model class
            context.Movie.AddRange(
                new Movie
                {
                    MovieName = "The Dark Knight",
                    Genre = "Action",
                    ReleaseYear = 2008,
                    EntryDate = DateTime.Parse("2023-6-29"),
                    JournalEntry = "I was enthralled by the complexity of the characters, particularly the Joker. The edginess of the plot kept me on the edge of my seat throughout, it truly is one of the greatest superhero films ever made."
                },
                new Movie
                {
                    MovieName = "Interstellar",
                    Genre = "Sci-Fi",
                    ReleaseYear = 2014,
                    EntryDate = DateTime.Parse("2023-6-24"),
                    JournalEntry = "This is one of, if not my favorite movie of all time. The film takes me on an emotional rollercoaster every time, with its captivating depiction of love transcending time and space. The complex science keeps me engaged, and the spectacular visuals keep me dreaming of a home in the stars."
                },
                new Movie
                {
                    MovieName = "Titanic",
                    Genre = "Drama",
                    ReleaseYear = 1997,
                    EntryDate = DateTime.Parse("2023-6-20"),
                    JournalEntry = "The emotional depth of the characters and the tragic end left a lump in my throat. I appreciated the historical authenticity, even though the romantic storyline felt somewhat clichéd."
                },
                new Movie
                {
                    MovieName = "The Matrix",
                    Genre = "Action",
                    ReleaseYear = 1999,
                    EntryDate = DateTime.Parse("2023-6-17"),
                    JournalEntry = "The mind-bending concept made me reflect on my perception of reality. The action scenes were exhilarating, but the philosophical undertones truly set this movie apart."
                },
                new Movie
                {
                    MovieName = "The Lion King",
                    Genre = "Adventure",
                    ReleaseYear = 1994,
                    EntryDate = DateTime.Parse("2023-6-16"),
                    JournalEntry = "The nostalgic charm and powerful storytelling resonated with me deeply. The vivid animation and memorable music brought joy, despite the heavy themes of loss and responsibility."
                }


            );
            context.SaveChanges();
        }
    }
}