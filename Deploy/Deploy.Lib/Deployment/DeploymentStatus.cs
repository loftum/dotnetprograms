using System;
using System.Collections.Generic;

namespace Deploy.Lib.Deployment
{
    [Serializable]
    public class DeploymentStatus
    {
        public List<DeploymentStepStatus> StepStatuses{ get; set;}

        public DeploymentStatus()
        {
            StepStatuses = new List<DeploymentStepStatus>();
        }

        public void Add(DeploymentStepStatus status)
        {
            StepStatuses.Add(status);
        }
    }
}
