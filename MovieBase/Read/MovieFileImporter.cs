using System;
using System.Collections.Generic;
using System.IO;
using MovieBase.Data.Services;

namespace Read
{
    public class MovieFileImporter
    {
        private readonly string _filePath;
        private readonly IMovieBaseService _movieBaseService;
        private readonly IDictionary<string, ImportedMovie> _importedMovies;
        private readonly IList<string> _duplicateTitles;

        public MovieFileImporter(string filePath, IMovieBaseService movieBaseService)
        {
            if (!File.Exists(filePath))
            {
                throw new MovieReaderException(filePath + " does not exist");
            }
            _filePath = filePath;
            _movieBaseService = movieBaseService;
            _importedMovies = new Dictionary<string, ImportedMovie>();
            _duplicateTitles = new List<string>();
        }

        public void ImportAll()
        {
            ParseAndAddAll();
            DoImportAll();
            PrintDuplicateTitles();
            Cleanup();
        }

        private void PrintDuplicateTitles()
        {
            Console.WriteLine();
            Console.WriteLine("Duplicates:");
            foreach (var duplicateTitle in _duplicateTitles)
            {
                Console.WriteLine(duplicateTitle);
            }
        }

        private void Cleanup()
        {
            _importedMovies.Clear();
        }

        private void ParseAndAddAll()
        {
            using (var stream = File.OpenRead(_filePath))
            {
                using (var reader = new StreamReader(stream))
                {
                    while(!reader.EndOfStream)
                    {
                        ParseAndAdd(reader.ReadLine());
                    }
                }
            }
        }

        private void DoImportAll()
        {
            foreach (var importedMovie in _importedMovies.Values)
            {
                var movie = _movieBaseService.MovieByTitle(importedMovie.Title);
                foreach (var category in importedMovie.Categories)
                {
                    movie.AddCategory(_movieBaseService.CategoryByName(category));
                }
                _movieBaseService.Save(movie);
                Console.WriteLine("Imported '" + movie.Title + "'");
            }
            Console.WriteLine("Imported " + _importedMovies.Count + " movies successfully.");
        }

        private void ParseAndAdd(string line)
        {
            var importedMovie = Parse(line);
            if (importedMovie == null)
            {
                return;
            }
            if (_importedMovies.ContainsKey(importedMovie.Title))
            {
                _duplicateTitles.Add(importedMovie.Title);
            }
            _importedMovies[importedMovie.Title] = importedMovie;
        }

        private static ImportedMovie Parse(string line)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                return null;
            }
            var split = line.Split(new[] { ';' }, 2);
            if (split.Length == 0)
            {
                return null;
            }
            var importedMovie = new ImportedMovie {Title = GetTitle(split)};
            foreach(var category in GetCategories(split))
            {
                importedMovie.AddCategory(category);
            }
            return importedMovie;
        }

        private static IEnumerable<string> GetCategories(IList<string> split)
        {
            return split.Count < 2 ?
                new string[0] :
                split[1].Split(new[] {'/'});
        }

        private static string GetTitle(IList<string> split)
        {
            return split[0];
        }
    }
}