using Microsoft.AspNetCore.Authorization;

namespace ApiWithAuhtenticationBearer.PolicyAuthorization
{
    public class MinimumAgeRequirement : IAuthorizationRequirement//tutaj musi byc aspnetcore.authorization
    {
        public int MinimumAge { get; }

        public MinimumAgeRequirement(int minimumAge)
        {
            MinimumAge = minimumAge;
        }
    }
}
