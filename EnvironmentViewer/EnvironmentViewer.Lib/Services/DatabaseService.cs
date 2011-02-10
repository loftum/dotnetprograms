using System;
using EnvironmentViewer.Lib.Data;
using EnvironmentViewer.Lib.Domain;
using EnvironmentViewer.Lib.SessionFactories;

namespace EnvironmentViewer.Lib.Services
{
    public class DatabaseService : IDatabaseService
    {
        private readonly IVersionRepoProvider _provider;

        public DatabaseService(IVersionRepoProvider provider)
        {
            _provider = provider;
        }

        public DatabaseState GetDatabaseState(EnvironmentData environmentData)
        {
            var state = new DatabaseState();
            var credentials = new DatabaseCredentials
            {
                DatabaseType = environmentData.DatabaseType,
                Host = environmentData.DatabaseHost,
                Database = environmentData.DatabaseName,
                Username = environmentData.DatabaseUsername,
                Password = environmentData.DatabasePassword,
                IntegratedSecurity = environmentData.IntegratedSecurity
            };

            state.Status = _provider.TestConnection(credentials);

            if (credentials.IsValid)
            {
                try
                {
                    using (var repo = _provider.GetVersionRepo(credentials))
                    {
                        state.Version = repo.GetVersion().ToString();
                    }
                }
                catch (Exception e)
                {
                    state.Version = "No SchemaInfo";
                }
            }
            else
            {
                state.Status = "Invalid credentials";
            }

            return state;
        }
    }
}