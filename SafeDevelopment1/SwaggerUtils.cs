using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace CardStorageService
{
    public static class SwaggerUtils
    {
        public static string OperationIdProvider(ApiDescription description)
        {
            string controllerName = description.ActionDescriptor.RouteValues["controller"];
            string actionName = description.ActionDescriptor.Id;

            if (description.TryGetMethodInfo(out MethodInfo methodInfo))
            {
                actionName = methodInfo.Name;
                if (actionName.EndsWith("Async"))
                {
                    actionName = actionName.Substring(0, actionName.Length - 5);
                }
            }

            return $"{controllerName}_{actionName}";
        }
    }
}
