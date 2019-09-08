using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using LY.PF.Auditing.Dto;
using LY.PF.Dto;

namespace LY.PF.Auditing
{
    public interface IAuditLogAppService : IApplicationService
    {
        Task<PagedResultDto<AuditLogListDto>> GetAuditLogs(GetAuditLogsInput input);

        Task<FileDto> GetAuditLogsToExcel(GetAuditLogsInput input);
    }
}