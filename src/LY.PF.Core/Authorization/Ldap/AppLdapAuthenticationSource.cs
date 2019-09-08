using Abp.Zero.Ldap.Authentication;
using Abp.Zero.Ldap.Configuration;
using LY.PF.Authorization.Users;
using LY.PF.MultiTenancy;

namespace LY.PF.Authorization.Ldap
{
    public class AppLdapAuthenticationSource : LdapAuthenticationSource<Tenant, User>
    {
        public AppLdapAuthenticationSource(ILdapSettings settings, IAbpZeroLdapModuleConfig ldapModuleConfig)
            : base(settings, ldapModuleConfig)
        {
        }
    }
}
