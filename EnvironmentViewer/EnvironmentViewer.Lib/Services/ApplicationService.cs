using System;
using System.Net;
using EnvironmentViewer.Lib.Domain;
using EnvironmentViewer.Lib.Extensions;

namespace EnvironmentViewer.Lib.Services
{
    public class ApplicationService : IApplicationService
    {
        public ApplicationState GetApplicationState(EnvironmentData environmentData)
        {
            var state = new ApplicationState();
            if (environmentData.Url.IsNullOrEmpty())
            {
                state.Status = "N/A";
                state.Version = "N/A";
                return state;
            }

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
                    state.Status = response != null ?
                        response.StatusCode.ToString("D") :
                        e.Message;
                }
                catch (Exception e)
                {
                    state.Status = e.GetType().Name + ": " +  e.Message;
                }
            }
            else
            {
                state.Status = "Invalid url";
            }
            return state;
        }
    }
}