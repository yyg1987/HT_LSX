using System.Threading.Tasks;
using Abp;
using Abp.Notifications;
using LY.PF.Authorization.Users;
using LY.PF.MultiTenancy;

namespace LY.PF.Notifications
{
    public interface IAppNotifier
    {
        Task WelcomeToTheApplicationAsync(User user);

        Task NewUserRegisteredAsync(User user);

        Task NewTenantRegisteredAsync(Tenant tenant);

        Task SendMessageAsync(UserIdentifier user, string message, NotificationSeverity severity = NotificationSeverity.Info);
    }
}
