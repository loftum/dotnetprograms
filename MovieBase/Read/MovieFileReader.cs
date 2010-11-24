using System;
using System.Collections.Generic;
using System.IO;
using MovieBase.Domain;

namespace Read
{
    public class MovieFileReader
    {
        private readonly string _filePath;

        public MovieFileReader(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new MovieReaderException(filePath + " does not exist");
            }
            _filePath = filePath;
        }

        public IList<Movie> ReadAll()
        {
            var movies = new List<Movie>();
            using (var stream = File.OpenRead(_filePath))
            {
                using (var reader = new StreamReader(stream))
                {
                    while(!reader.EndOfStream)
                    {
                        var movie = Parse(reader.ReadLine());
                        if (movie != null)
                        {
                            movies.Add(movie);
                        }
                    }
                }
            }
            return null;
        }

        private static Movie Parse(string line)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                return null;
            }
            var split = line.Split(new[] {';'}, 2);
            if (split.Length == 0)
            {
                return null;
            }
            var title = GetTitle(split);
            var movie = new Movie();
            movie.Title = title;
            foreach (var category in GetCategories(split))
            {
                movie.AddCategory(new Category{Name = category});
            }
            return movie;
        }

        private static IEnumerable<string> GetCategories(string[] split)
        {
            if (split.Length < 2)
            {
                return new string[0];
            }
            return split[1].Split(new[] {'/'});
        }

        private static string GetTitle(IList<string> split)
        {
            return split[0];
        }
    }
}