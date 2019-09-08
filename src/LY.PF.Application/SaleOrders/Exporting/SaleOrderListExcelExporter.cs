using System;
using System.Collections.Generic;
using LY.PF.SaleOrders.Dtos;
using LY.PF.DataExporting.Excel.EpPlus;
using Abp.Timing.Timezone;
using Abp.Runtime.Session;
using LY.PF.Dto;

namespace LY.PF.SaleOrders
{
    /// <summary>
    /// 订单的导出EXCEL功能的实现
    /// </summary>
    public class SaleOrderListExcelExporter : EpPlusExcelExporterBase, ISaleOrderListExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;


        /// <summary>
        /// 构造方法
        /// </summary>
        public SaleOrderListExcelExporter(ITimeZoneConverter timeZoneConverter, IAbpSession abpSession)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }



        /// <summary>
        /// 导出订单到EXCEL文件
        /// <param name="saleOrderListDtos">导出信息的DTO</param>
        /// </summary>
        public FileDto ExportSaleOrderToFile(List<SaleOrderListDto> saleOrderListDtos)
        {


            var file = CreateExcelPackage("saleOrderList.xlsx", excelPackage =>
            {

                var sheet = excelPackage.Workbook.Worksheets.Add(L("SaleOrder"));
                sheet.OutLineApplyStyle = true;

                AddHeader(
                    sheet,
                     L("ProductType"),
                     L("ScheduleNumber"),
                     L("ActualNumber"),
                     L("Price"),
                     L("TotalPrice"),
                     L("Status"),
                     L("CreateTime"),
                     L("UpdateTime")

                    );
                AddObjects(sheet, 2, saleOrderListDtos,

             _ => _.ProductType,

             _ => _.ScheduleNumber,

             _ => _.ActualNumber,

             _ => _.Price,

             _ => _.TotalPrice,

             _ => _.Status,

            _ => _timeZoneConverter.Convert(_.CreateTime, _abpSession.TenantId, _abpSession.GetUserId()),
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
