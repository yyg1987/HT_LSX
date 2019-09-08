using System.Collections.Generic;
using LY.PF.Authorization.Users.Dto;

namespace LY.PF.Web.Areas.Mpa.Models.Users
{
    public class UserLoginAttemptModalViewModel
    {
        public List<UserLoginAttemptDto> LoginAttempts { get; set; }
    }
}