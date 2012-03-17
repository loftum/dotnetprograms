using VisualFarmStudio.Lib.Model;

namespace VisualFarmStudio.Lib.UserSession
{
    public class UserContext
    {
        public BondeModel Bonde { get; private set; }

        public UserContext(BondeModel bonde)
        {
            Bonde = bonde;
        }
    }
}