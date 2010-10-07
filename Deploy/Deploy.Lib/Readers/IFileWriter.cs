namespace Deploy.Lib.Readers
{
    public interface IFileWriter
    {
        IFileWriter Write(string text, string filePath);
    }
}
