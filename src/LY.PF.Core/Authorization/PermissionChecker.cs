using Abp.Authorization;
using LY.PF.Authorization.Roles;
using LY.PF.Authorization.Users;
using LY.PF.MultiTenancy;

namespace LY.PF.Authorization
{
    /// <summary>
    /// Implements <see cref="PermissionChecker"/>.
    /// </summary>
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
