using System.Linq;
using System.Web;
using System.Web.Mvc;
using VisualFarmStudio.Common.Scoping;
using VisualFarmStudio.Lib.UserSession;

namespace VisualFarmStudio.Attributes
{
    public class VFSAuthorizeAttribute : AuthorizeAttribute
    {
        public string[] RolesArray { get; set; }

        public VFSAuthorizeAttribute(string[] roles)
        {
            RolesArray = roles;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var userManager = InjectionContainer.Get<IUserManager>();
            if (!userManager.IsLoggedIn)
            {
                return false;
            }

            var roles = userManager.CurrentUser.Bonde.Rolles.Select(r => r.Kode);
            return RolesArray.Any(roles.Contains);
        }
    }
}