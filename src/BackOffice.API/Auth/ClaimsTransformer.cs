using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace Backoffice.API.Auth;

public class ClaimsTransformer : IClaimsTransformation
{
    public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        ClaimsIdentity claimsIdentity = (ClaimsIdentity)principal.Identity;

        // flatten resource_access because Microsoft identity model doesn't support nested claims
        // by map it to Microsoft identity model, because automatic JWT bearer token mapping already processed here
        if (claimsIdentity.IsAuthenticated && claimsIdentity.HasClaim((claim) => claim.Type == "resource_access"))
        {
            var userRole = claimsIdentity.FindFirst((claim) => claim.Type == "resource_access");

            var content = Newtonsoft.Json.Linq.JObject.Parse(userRole.Value);

            foreach (var role in content["backoffice"]["roles"])
            {
                claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role.ToString()));
            }
        }

        return Task.FromResult(principal);
    }
}