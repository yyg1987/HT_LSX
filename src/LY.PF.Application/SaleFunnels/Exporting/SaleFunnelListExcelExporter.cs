using System.Collections.Generic;
using LY.PF.SaleFunnels.Dtos;
using LY.PF.DataExporting.Excel.EpPlus;
using Abp.Timing.Timezone;
using Abp.Runtime.Session;
using LY.PF.Dto;

namespace LY.PF.SaleFunnels
{
    /// <summary>
    /// 销售漏斗的导出EXCEL功能的实现
    /// </summary>
    public class SaleFunnelListExcelExporter : EpPlusExcelExporterBase, ISaleFunnelListExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;


        /// <summary>
        /// 构造方法
        /// </summary>
        public SaleFunnelListExcelExporter(ITimeZoneConverter timeZoneConverter, IAbpSession abpSession)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }



        /// <summary>
        /// 导出销售漏斗到EXCEL文件
        /// <param name="saleFunnelListDtos">导出信息的DTO</param>
        /// </summary>
        public FileDto ExportSaleFunnelToFile(List<SaleFunnelListDto> saleFunnelListDtos)
        {


            var file = CreateExcelPackage("saleFunnelList.xlsx", excelPackage =>
            {

                var sheet = excelPackage.Workbook.Worksheets.Add(L("SaleFunnel"));
                sheet.OutLineApplyStyle = true;

                AddHeader(
                    sheet,
                     L("ClientType"),
                     L("ProductType"),
                     L("Number"),
                     L("StatementTime"),
                     L("ContendNumber"),
                     L("StageTime"),
                     L("LastTime"),
                     L("NextTime"),
                     L("CreationTime"),
                     L("UpdateTime")
                    );
                AddObjects(sheet, 2, saleFunnelListDtos,

             _ => _.ClientType,

             _ => _.ProductType,

             _ => _.Number,

        _ => _timeZoneConverter.Convert(_.StatementTime, _abpSession.TenantId, _abpSession.GetUserId()),
             _ => _.ContendNumber,

        _ => _timeZoneConverter.Convert(_.StageTime, _abpSession.TenantId, _abpSession.GetUserId()),
        _ => _timeZoneConverter.Convert(_.LastTime, _abpSession.TenantId, _abpSession.GetUserId()),
        _ => _timeZoneConverter.Convert(_.NextTime, _abpSession.TenantId, _abpSession.GetUserId()),
        _ => _timeZoneConverter.Convert(_.CreationTime, _abpSession.TenantId, _abpSession.GetUserId()),
        _ => _timeZoneConverter.Convert(_.UpdateTime, _abpSession.TenantId, _abpSession.GetUserId())
       );
                //写个时间转换的吧
                //var creationTimeColumn = sheet.Column(10);
                //creationTimeColumn.Style.Numberformat.Format = "yyyy-mm-dd";

                for (var i = 1; i <= 10; i++)
                {
                    sheet.Column(i).AutoFit();
                }

            });
            return file;

        }
    }
}
