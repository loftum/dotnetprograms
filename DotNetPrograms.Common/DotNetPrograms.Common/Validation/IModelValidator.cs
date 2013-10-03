using System.Collections.Generic;

namespace DotNetPrograms.Common.Validation
{
    public interface IModelValidator
    {
        IList<PropertyError> Errors { get; }
    }
}