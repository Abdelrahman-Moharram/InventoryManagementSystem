using InventoryManagementSystem.Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem.Infrastructure.Filters
{
    public class PermissionPolicyProvider : IAuthorizationPolicyProvider
    {
        public DefaultAuthorizationPolicyProvider FallBackPolicyProvider { get; }

        public PermissionPolicyProvider(IOptions<AuthorizationOptions> options)
        {
            FallBackPolicyProvider = new DefaultAuthorizationPolicyProvider(options);
        }
        public Task<AuthorizationPolicy> GetDefaultPolicyAsync()
        {
            return FallBackPolicyProvider.GetDefaultPolicyAsync();
        }

        public Task<AuthorizationPolicy?> GetFallbackPolicyAsync()
        {
            return FallBackPolicyProvider.GetDefaultPolicyAsync();
        }

        public Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
        {
            if (policyName.StartsWith(OtherConstants.Permissions.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                var policy = new AuthorizationPolicyBuilder();
                policy.AddRequirements(new PermissionRequirement(policyName));
                return Task.FromResult(policy.Build());
            }
            return FallBackPolicyProvider.GetPolicyAsync(policyName);
        }
    }
}
