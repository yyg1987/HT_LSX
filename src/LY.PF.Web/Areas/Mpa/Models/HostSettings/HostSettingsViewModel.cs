﻿using System.Collections.Generic;
using Abp.Application.Services.Dto;
using LY.PF.Configuration.Host.Dto;

namespace LY.PF.Web.Areas.Mpa.Models.HostSettings
{
    public class HostSettingsViewModel
    {
        public HostSettingsEditDto Settings { get; set; }

        public List<ComboboxItemDto> EditionItems { get; set; }

        public List<ComboboxItemDto> TimezoneItems { get; set; }
    }
}