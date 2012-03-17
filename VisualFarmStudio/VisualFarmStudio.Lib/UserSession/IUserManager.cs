using VisualFarmStudio.Lib.Model;

namespace VisualFarmStudio.Lib.UserSession
{
    public interface IUserManager
    {
        UserContext CurrentUser { get; }
        bool IsLoggedIn { get; }
        void LogIn(BondeModel bonde);
        void LogOut();
    }
}