namespace VisualFarmStudio.Lib.UserSession
{
    public interface ISessionManager
    {
        UserContext UserContext { get; set; }
        void Abandon();
    }
}