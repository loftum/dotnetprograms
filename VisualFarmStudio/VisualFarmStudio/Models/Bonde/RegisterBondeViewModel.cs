using VisualFarmStudio.Lib.Model;

namespace VisualFarmStudio.Models.Bonde
{
    public class RegisterBondeViewModel
    {
        public BondeModel Bonde { get; set; }

        public RegisterBondeViewModel(){}

        public RegisterBondeViewModel(BondeModel bonde)
        {
            Bonde = bonde;
        }
    }
}