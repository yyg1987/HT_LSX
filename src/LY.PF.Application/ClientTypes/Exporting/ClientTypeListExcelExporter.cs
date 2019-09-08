using System;
using System.Collections.Generic;
using LY.PF.ClientTypes.Dtos;
using LY.PF.DataExporting.Excel.EpPlus;
using Abp.Timing.Timezone;
using Abp.Runtime.Session;
using LY.PF.Dto;

namespace LY.PF.ClientTypes
{
    /// <summary>
    /// 订单的导出EXCEL功能的实现
    /// </summary>
    public class ClientTypeListExcelExporter : EpPlusExcelExporterBase, IClientTypeListExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;


        /// <summary>
        /// 构造方法
        /// </summary>
        public ClientTypeListExcelExporter(ITimeZoneConverter timeZoneConverter, IAbpSession abpSession)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }



        /// <summary>
        /// 导出订单到EXCEL文件
        /// <param name="clientTypeListDtos">导出信息的DTO</param>
        /// </summary>
        public FileDto ExportClientTypeToFile(List<ClientTypeListDto> clientTypeListDtos)
        {


            var file = CreateExcelPackage("clientTypeList.xlsx", excelPackage =>
            {

                var sheet = excelPackage.Workbook.Worksheets.Add(L("ClientType"));
                sheet.OutLineApplyStyle = true;

                AddHeader(
                    sheet,
                     L("ClientTypeName"),
                     L("Remark"),
                     L("IsValid"),
                     L("CreateBy"),
                     L("CreationTime"),
                     L("UpdateBy"),
                     L("UpdateTime")

                    );
                AddObjects(sheet, 2, clientTypeListDtos,

             _ => _.ClientTypeName,

             _ => _.Remark,

             _ => _.IsValid,

             _ => _.CreateBy,
            _ => _timeZoneConverter.Convert(_.CreationTime, _abpSession.TenantId, _abpSession.GetUserId()),

             _ => _.UpdateBy,

            _ => _timeZoneConverter.Convert(_.UpdateTime, _abpSession.TenantId, _abpSession.GetUserId())
           );
                //写个时间转换的吧
                //var creationTimeColumn = sheet.Column(10);
                //creationTimeColumn.Style.Numberformat.Format = "yyyy-mm-dd";

                for (var i = 1; i <= 8; i++)
                {
                    sheet.Column(i).AutoFit();
                }

            });
            return file;

        }
    }
}
