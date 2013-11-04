using System;
using DotNetPrograms.Common.Validation;

namespace MasterData.Core.Model.Stores
{
    public class EditSalespointModel : MasterDataObjectModel
    {
        public Guid ResellerId { get; set; }
        public string ResellerName { get; set; }
        public string Identifier { get; set; }
        public string Name { get; set; }

        public string DisplayName
        {
            get { return IsNew ? "New salespoint" : Name; }
        }

        public void UpdateFrom(EditSalespointModel input)
        {
            Name = input.Name;
            Identifier = input.Identifier;
        }

        public ModelValidator<EditSalespointModel> Validate()
        {
            return new ModelValidator<EditSalespointModel>(this)
                .Require(m => m.Identifier)
                .Require(m => m.Name);
        }
    }
}