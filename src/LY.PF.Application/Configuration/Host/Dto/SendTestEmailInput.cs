using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using LY.PF.Authorization.Users;

namespace LY.PF.Configuration.Host.Dto
{
    public class SendTestEmailInput
    {
        [Required]
        [MaxLength(User.MaxEmailAddressLength)]
        public string EmailAddress { get; set; }
    }
}