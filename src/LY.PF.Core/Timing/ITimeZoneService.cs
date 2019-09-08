using System.Threading.Tasks;
using Abp.Configuration;

namespace LY.PF.Timing
{
    public interface ITimeZoneService
    {
        Task<string> GetDefaultTimezoneAsync(SettingScopes scope, int? tenantId);
    }
}
