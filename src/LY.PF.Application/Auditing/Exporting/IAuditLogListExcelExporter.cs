using System.Collections.Generic;
using LY.PF.Auditing.Dto;
using LY.PF.Dto;

namespace LY.PF.Auditing.Exporting
{
    public interface IAuditLogListExcelExporter
    {
        FileDto ExportToFile(List<AuditLogListDto> auditLogListDtos);
    }
}
