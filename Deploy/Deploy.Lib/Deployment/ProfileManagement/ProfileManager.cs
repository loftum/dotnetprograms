using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Deploy.Lib.Configuration;
using Deploy.Lib.Deployment.Profiles;

namespace Deploy.Lib.Deployment.ProfileManagement
{
    public class ProfileManager : IProfileManager
    {
        private static ProfileManager _instance;
        private readonly IDictionary<string, DeploymentProfile> _profiles = new Dictionary<string, DeploymentProfile>();

        public static ProfileManager Instance
        {
            get { return _instance ?? (_instance = new ProfileManager()); }
        }

        private ProfileManager()
        {
            ReadProfiles();
        }

        private void ReadProfiles()
        {
            var profileDirectory = new DirectoryInfo(DeploymentConfiguration.ProfileFolder);
            if (!profileDirectory.Exists)
            {
                profileDirectory.Create();
                return;
            }
            foreach(var file in profileDirectory.GetFiles("*.xml"))
            {
                var serializer = new XmlSerializer(typeof (DeploymentProfile));
                try
                {
                    using (var stream = file.OpenRead())
                    {
                        var deploymentProfile = (DeploymentProfile)serializer.Deserialize(stream);
                        _profiles.Add(deploymentProfile.Name, deploymentProfile);
                    }
                }
                catch (InvalidOperationException)
                {
                    
                }
            }
        }

        public IEnumerable<DeploymentProfile> GetAll()
        {
            return _profiles.Values;
        }

        public void Add(DeploymentProfile deploymentProfile)
        {
            VerifyNoProfileWithSameName(deploymentProfile);
            _profiles.Add(deploymentProfile.Name, deploymentProfile);
        }

        public void Save(DeploymentProfile deploymentProfile)
        {
            var profileDirectory = new DirectoryInfo(DeploymentConfiguration.ProfileFolder);
            if (!profileDirectory.Exists)
            {
                profileDirectory.Create();
            }
            var profileFilename = Path.Combine(profileDirectory.FullName, deploymentProfile.Name + ".xml");
            var serializer = new XmlSerializer(typeof(DeploymentProfile));
            try
            {
                using (var stream = File.Create(profileFilename, 4096, FileOptions.None))
                {
                    serializer.Serialize(stream, deploymentProfile);
                }
            }
            catch (InvalidOperationException e)
            {
                throw new DeploymentProfileException("Could not save " + deploymentProfile.Name, e);
            }
        }

        public DeploymentProfile Get(string profileName)
        {
            if (string.IsNullOrEmpty(profileName) || !Has(profileName))
            {
                throw new DeploymentProfileException(profileName + " does not exist");
            }
            return _profiles[profileName];
        }

        public bool Has(string profileName)
        {
            return _profiles.ContainsKey(profileName);
        }

        public void VerifyNoProfileWithSameName(DeploymentProfile deploymentProfile)
        {
            if (Has(deploymentProfile.Name))
            {
                throw new DeploymentProfileException("Profile " + deploymentProfile.Name + " already exists");
            }
        }
    }
}
