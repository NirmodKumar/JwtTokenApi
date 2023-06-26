using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace JwtTokenApi.CustomAttributes
{
    public class RequiredClaimAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string _claimName;
        private readonly string _claimValue;

        public RequiredClaimAttribute(string claimName, string claimValue)
        {
            _claimName = claimName ?? throw new ArgumentNullException(nameof(claimName));
            _claimValue = claimValue ?? throw new ArgumentNullException(nameof(claimValue));
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.HasClaim(_claimName, _claimValue))
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
