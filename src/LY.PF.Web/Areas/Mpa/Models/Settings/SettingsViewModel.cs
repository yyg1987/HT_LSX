using System.Collections.Generic;
using Abp.Application.Services.Dto;
using LY.PF.Configuration.Tenants.Dto;

namespace LY.PF.Web.Areas.Mpa.Models.Settings
{
    public class SettingsViewModel
    {
        public TenantSettingsEditDto Settings { get; set; }
        
        public List<ComboboxItemDto> TimezoneItems { get; set; }
    }
}