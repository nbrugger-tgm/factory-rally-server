using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tgm.Roborally.Server.Authentication
{
    public class ApiKeyRequirement : IAuthorizationRequirement
    {
        public IReadOnlyList<string> ApiKeys { get; set; }
        
        public string PolicyName { get; set; }

        public ApiKeyRequirement(IEnumerable<string> apiKeys, string policyName)
        {
            ApiKeys = apiKeys?.ToList() ?? new List<string>();
            PolicyName = policyName;
        }
    }

    public class ApiKeyRequirementHandler : AuthorizationHandler<ApiKeyRequirement>
    {

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ApiKeyRequirement requirement)
        {
            SucceedRequirementIfApiKeyPresentAndValid(context, requirement);
            return Task.CompletedTask;
        }

        private void SucceedRequirementIfApiKeyPresentAndValid(AuthorizationHandlerContext context, ApiKeyRequirement requirement)
        {

            if (context.Resource is AuthorizationFilterContext authorizationFilterContext)
            {
                var apiKey = "";
                apiKey = authorizationFilterContext.HttpContext.Request.Query["hid"].FirstOrDefault();
                if (requirement.PolicyName == "Host-token-access" && apiKey != null && requirement.ApiKeys.Any(requiredApiKey => apiKey == requiredApiKey))
                {
                    context.Succeed(requirement);
                }

                apiKey = authorizationFilterContext.HttpContext.Request.Query["uid"].FirstOrDefault();
                if (requirement.PolicyName == "Player-Token-Access" && apiKey != null && requirement.ApiKeys.Any(requiredApiKey => apiKey == requiredApiKey))
                {
                    context.Succeed(requirement);
                }
            }

        }
    }
}

