using System;
using System.Collections.Generic;
using LY.PF.Districts.Dtos;
using LY.PF.DataExporting.Excel.EpPlus;
using Abp.Timing.Timezone;
using Abp.Runtime.Session;
using LY.PF.Dto;

namespace LY.PF.Districts
{
    /// <summary>
    /// 订单的导出EXCEL功能的实现
    /// </summary>
    public class DistrictListExcelExporter : EpPlusExcelExporterBase, IDistrictListExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;


        /// <summary>
        /// 构造方法
        /// </summary>
        public DistrictListExcelExporter(ITimeZoneConverter timeZoneConverter, IAbpSession abpSession)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }



        /// <summary>
        /// 导出订单到EXCEL文件
        /// <param name="districtListDtos">导出信息的DTO</param>
        /// </summary>
        public FileDto ExportDistrictToFile(List<DistrictListDto> districtListDtos)
        {


            var file = CreateExcelPackage("districtList.xlsx", excelPackage =>
            {

                var sheet = excelPackage.Workbook.Worksheets.Add(L("District"));
                sheet.OutLineApplyStyle = true;

                AddHeader(
                    sheet,
                     L("DistrictName"),
                     L("Address"),
                     L("Remark"),
                     L("IsValid"),
                     L("ParentDistrictId"),
                     L("CreateBy"),
                     L("CreationTime"),
                     L("UpdateBy"),
                     L("UpdateTime")

                    );
                AddObjects(sheet, 2, districtListDtos,

             _ => _.DistrictName,

             _ => _.Address,

             _ => _.Remark,

             _ => _.IsValid,

             _ => _.ParentDistrictId,

             _ => _.CreateBy,

            _ => _timeZoneConverter.Convert(_.CreationTime, _abpSession.TenantId, _abpSession.GetUserId()),
            _ => _.CreateBy,
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
