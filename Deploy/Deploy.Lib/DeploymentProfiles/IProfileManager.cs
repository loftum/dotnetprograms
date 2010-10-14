using System.Collections.Generic;

namespace Deploy.Lib.DeploymentProfiles
{
    public interface IProfileManager
    {
        IEnumerable<DeploymentProfile> GetAll();
        void Add(DeploymentProfile deploymentProfile);
        void Save(DeploymentProfile deploymentProfile);
    }
}