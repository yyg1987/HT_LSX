using System.Collections.Generic;
using LY.PF.Caching.Dto;

namespace LY.PF.Web.Areas.Mpa.Models.Maintenance
{
    public class MaintenanceViewModel
    {
        public IReadOnlyList<CacheDto> Caches { get; set; }
    }
}