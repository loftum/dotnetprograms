using VisualFarmStudio.Lib.Model;

namespace VisualFarmStudio.Lib.Facades
{
    public interface IBondeFacade
    {
        void Add(BondeModel model);
        bool IsTaken(string brukernavn);
        BondeModel Get(string brukernavn);
    }
}