using System;
using System.Net;
using EnvironmentViewer.Lib.Domain;

namespace EnvironmentViewer.Lib.Services
{
    public class ApplicationService : IApplicationService
    {
        public ApplicationState GetApplicationState(EnvironmentData environmentData)
        {
            var state = new ApplicationState();

            if (environmentData.HasValidUrl)
            {
                try
                {
                    var request = WebRequest.Create(environmentData.Url);
                    var response = (HttpWebResponse)request.GetResponse();
                    if (response != null)
                    {
                        state.Status = response.StatusCode.ToString("D");
                    }
                }
                catch(WebException e)
                {
                    var response = (HttpWebResponse) e.Response;
                    if (response != null)
                    {
                        state.Status = response.StatusCode.ToString("D");
                    }
                    else
                    {
                        state.Status = e.Message;
                    }
                }
                catch (Exception e)
                {
                    state.Status = e.GetType().Name + ": " +  e.Message;
                }
            }
            return state;
        }
    }
}