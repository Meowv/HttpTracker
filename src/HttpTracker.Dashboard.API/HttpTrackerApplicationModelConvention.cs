using HttpTracker.Controller;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Linq;

namespace HttpTracker
{
    public class HttpTrackerApplicationModelConvention : IApplicationModelConvention
    {
        public void Apply(ApplicationModel application)
        {
            foreach (var controller in application.Controllers)
            {
                if (controller.ControllerType == typeof(HttpTrackerController))
                {
                    controller.ApiExplorer.GroupName = "HttpTrackerDashboardAPI";
                    controller.ApiExplorer.IsVisible = true;

                    foreach (var action in controller.Actions)
                    {
                        var route = (action.Attributes.FirstOrDefault(x => x.GetType() == typeof(RouteAttribute)) as RouteAttribute)?.Template;
                        if (string.IsNullOrEmpty(route))
                        {
                            route = action.ActionName;
                        }

                        action.ApiExplorer.IsVisible = true;
                        action.Selectors.First().AttributeRouteModel = new AttributeRouteModel(new RouteAttribute($"api/httptracker.{route}"));
                    }
                }
            }
        }
    }
}