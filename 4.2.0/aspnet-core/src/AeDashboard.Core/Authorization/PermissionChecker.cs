using Abp.Authorization;
using AeDashboard.Authorization.Roles;
using AeDashboard.Authorization.Users;

namespace AeDashboard.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
