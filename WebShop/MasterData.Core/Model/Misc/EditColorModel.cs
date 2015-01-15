using DotNetPrograms.Common.Validation;

namespace MasterData.Core.Model.Misc
{
    public class EditColorModel : MasterDataObjectModel
    {
        public string Name { get; set; }
        public string DisplayName { get { return IsNew ? "New color" : Name; } }
        public int Red { get; set; }
        public int Green { get; set; }
        public int Blue { get; set; }
        public decimal Alpha { get; set; }

        public void UpdateFrom(EditColorModel input)
        {
            Name = input.Name;
            Red = input.Red;
            Green = input.Green;
            Blue = input.Blue;
            Alpha = input.Alpha;
        }

        public ModelValidator<EditColorModel> Validate()
        {
            return new ModelValidator<EditColorModel>(this)
                .Require(m => m.Name)
                .Require(m => m.Red, To.BeWithin(0, 255))
                .Require(m => m.Green, To.BeWithin(0, 255))
                .Require(m => m.Blue, To.BeWithin(0, 255))
                .Require(m => m.Alpha, To.BeWithin(0m, 1m));
        }
    }
}