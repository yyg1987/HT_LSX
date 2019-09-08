using Abp.Notifications;
using LY.PF.Dto;

namespace LY.PF.Notifications.Dto
{
    public class GetUserNotificationsInput : PagedInputDto
    {
        public UserNotificationState? State { get; set; }
    }
}