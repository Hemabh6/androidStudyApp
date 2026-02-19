using StudySaaS.Api.Data;
using StudySaaS.Api.Services;

namespace StudySaaS.Api.Middleware;

public class TenantResolutionMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context, CurrentTenant tenant, InMemoryDataStore store)
    {
        var subdomain = context.Request.Headers["X-Tenant"].FirstOrDefault();

        if (!string.IsNullOrWhiteSpace(subdomain))
        {
            var institute = store.Institutes.FirstOrDefault(x => x.Subdomain.Equals(subdomain, StringComparison.OrdinalIgnoreCase));
            if (institute is not null)
            {
                tenant.Subdomain = institute.Subdomain;
                tenant.InstituteId = institute.Id;
            }
        }

        await next(context);
    }
}
