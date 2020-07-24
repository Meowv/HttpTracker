using HttpTracker.Controller;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Reflection;

namespace HttpTracker
{
    public class HttpTrackerControllerFeatureProvider : ControllerFeatureProvider
    {
        protected override bool IsController(TypeInfo typeInfo)
        {
            if (typeInfo == typeof(HttpTrackerController))
            {
                return true;
            }
            return base.IsController(typeInfo);
        }
    }
}