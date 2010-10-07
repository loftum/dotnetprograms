using System;
using System.Collections.Generic;

namespace Deploy.Lib.Deployment
{
    [Serializable]
    public class DeploymentStatus
    {
        public DateTime DeploymentTime { get; set; }
        public DeployParameters Parameters { get; set; }
        public List<DeploymentStepStatus> StepStatuses{ get; set;}

        public DeploymentStatus()
        {
            DeploymentTime = DateTime.Now;
            StepStatuses = new List<DeploymentStepStatus>();
        }

        public void Add(DeploymentStepStatus status)
        {
            StepStatuses.Add(status);
        }
    }
}
