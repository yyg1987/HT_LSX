using Abp.Application.Services;
using LY.PF.Dto;
using LY.PF.Logging.Dto;

namespace LY.PF.Logging
{
    public interface IWebLogAppService : IApplicationService
    {
        GetLatestWebLogsOutput GetLatestWebLogs();

        FileDto DownloadWebLogs();
    }
}
