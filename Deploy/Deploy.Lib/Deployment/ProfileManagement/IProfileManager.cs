using System.Collections.Generic;
using Deploy.Lib.Deployment.Profiles;

namespace Deploy.Lib.Deployment.ProfileManagement
{
    public interface IProfileManager
    {
        IEnumerable<DeploymentProfile> GetAll();
        void Add(DeploymentProfile deploymentProfile);
        void Save(DeploymentProfile deploymentProfile);
        DeploymentProfile Get(string profileName);
    }
}