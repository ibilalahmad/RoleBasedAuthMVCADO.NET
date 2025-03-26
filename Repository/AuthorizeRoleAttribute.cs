using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace RoleBasedAuth.Repository
{
    public class AuthorizeRoleAttribute : ActionFilterAttribute
    {
        private readonly string[] _roles;

        public AuthorizeRoleAttribute(params string[] roles)
        {
            _roles = roles;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var role = context.HttpContext.Session.GetString("Role");

            if (string.IsNullOrEmpty(role) || !_roles.Contains(role))
            {
                context.Result = new RedirectToActionResult("Login", "Account", null);
            }

            base.OnActionExecuting(context);
        }
    }
}
