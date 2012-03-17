namespace VisualFarmStudio.Lib.UserSession
{
    public interface ISession
    {
        void Write(string key, object value);
        void Abandon();
        T Read<T>(string userContextKey);
    }
}