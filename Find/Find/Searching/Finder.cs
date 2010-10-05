using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Find.Parameters;

namespace Find.Searching
{
    public class Finder
    {
        private readonly FindParameters _parameters;
        private readonly Regex _regex;

        public Finder(FindParameters parameters)
        {
            _parameters = parameters;
            _regex = new Regex(_parameters.Pattern);
        }

        public void Find()
        {
            if (!Directory.Exists(_parameters.Path))
            {
                throw new FindException(_parameters.Path + " does not exist");
            }
            Search(new DirectoryInfo(_parameters.Path));
        }

        private void Search(DirectoryInfo directoryInfo)
        {
            PrintIfMatch(directoryInfo);
            try
            {
                foreach (var fileInfo in directoryInfo.GetFiles().Where(Matches))
                {
                    PrintIfMatch(fileInfo);
                }
                foreach(var subFolder in directoryInfo.GetDirectories())
                {
                    Search(subFolder);
                }
            }
            catch (Exception e)
            {
                if (_parameters.ShowErrors)
                {
                    Console.WriteLine("Could not search " + directoryInfo.FullName + ": " + e.Message);    
                }
            }
        }

        private void PrintIfMatch(FileSystemInfo info)
        {
            if (Matches(info))
            {
                Console.WriteLine(info.FullName);
            }
        }

        private bool Matches(FileSystemInfo info)
        {
            return _regex.IsMatch(info.Name);
        }
    }
}
