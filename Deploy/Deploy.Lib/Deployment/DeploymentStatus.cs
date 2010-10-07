using System;
using System.Collections.Generic;
using System.Linq;

namespace Deploy.Lib.Deployment
{
    [Serializable]
    public class DeploymentStatus
    {
        public string Status { get; set; }
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
            CalculateStatus();
        }

        private void CalculateStatus()
        {
            var status = StepStatuses.Aggregate(DeploymentStepStatus.Ok, (current, stepStatus) => current & stepStatus.Status);
            switch (status)
            {
                case DeploymentStepStatus.Ok:
                    Status = "Successful";
                    break;
                case DeploymentStepStatus.Warning:
                    Status = "Warning";
                    break;
                case DeploymentStepStatus.Fail:
                    Status = "Failed";
                    break;
                default:
                    Status = "Failed";
                    break;
            }
        }
    }
}
