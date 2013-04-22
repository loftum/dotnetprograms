using System;

namespace BasicManifest.Core.Domain
{
    public interface IAuditable
    {
        DateTime CreatedDate { get; set; }
        string CreatedBy { get; set; }
        DateTime ModifiedDate { get; set; }
        string ModifiedBy { get; set; }
    }
}