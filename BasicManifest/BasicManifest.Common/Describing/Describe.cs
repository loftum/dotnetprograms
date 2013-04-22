namespace BasicManifest.Common.Describing
{
    public class Describe
    {
        public static string Type<T>()
        {
            return typeof (T).Name;
        }
    }
}