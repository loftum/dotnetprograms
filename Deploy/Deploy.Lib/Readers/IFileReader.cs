using System.Collections.Generic;

namespace Deploy.Lib.Readers
{
    public interface IFileReader
    {
        IEnumerable<string> ReadLines(string path);
        string ReadAll(string path);
    }
}
