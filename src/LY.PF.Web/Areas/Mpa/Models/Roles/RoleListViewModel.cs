using System.Collections.Generic;
using Abp.Application.Services.Dto;

namespace LY.PF.Web.Areas.Mpa.Models.Roles
{
    public class RoleListViewModel
    {
        public List<ComboboxItemDto> Permissions { get; set; }
    }
}