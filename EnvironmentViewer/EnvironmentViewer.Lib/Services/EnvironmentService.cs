using EnvironmentViewer.Lib.Domain;

namespace EnvironmentViewer.Lib.Services
{
    public class EnvironmentService : IEnvironmentService
    {
        private readonly IApplicationService _applicationService;
        private readonly IDatabaseService _databaseService;

        public EnvironmentService(IApplicationService applicationService, IDatabaseService databaseService)
        {
            _applicationService = applicationService;
            _databaseService = databaseService;
        }

        public EnvironmentState GetStateOf(EnvironmentData environmentData)
        {
            var state = new EnvironmentState();
            var databaseState = _databaseService.GetDatabaseState(environmentData);
            state.DatabaseStatus = databaseState.Status;
            state.DatabaseVersion = databaseState.Version;

            var applicationState = _applicationService.GetApplicationState(environmentData);
            state.ApplicationStatus = applicationState.Status;
            state.ApplicationVersion = applicationState.Version;

            return state;
        }
    }
}