using System;
using System.Collections.Generic;
using LY.PF.ProductTypes.Dtos;
using LY.PF.DataExporting.Excel.EpPlus;
using Abp.Timing.Timezone;
using Abp.Runtime.Session;
using LY.PF.Dto;

namespace LY.PF.ProductTypes
{
    /// <summary>
    /// 订单的导出EXCEL功能的实现
    /// </summary>
    public class ProductTypeListExcelExporter : EpPlusExcelExporterBase, IProductTypeListExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;


        /// <summary>
        /// 构造方法
        /// </summary>
        public ProductTypeListExcelExporter(ITimeZoneConverter timeZoneConverter, IAbpSession abpSession)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }



        /// <summary>
        /// 导出订单到EXCEL文件
        /// <param name="productTypeListDtos">导出信息的DTO</param>
        /// </summary>
        public FileDto ExportProductTypeToFile(List<ProductTypeListDto> productTypeListDtos)
        {


            var file = CreateExcelPackage("productTypeList.xlsx", excelPackage =>
            {

                var sheet = excelPackage.Workbook.Worksheets.Add(L("ProductType"));
                sheet.OutLineApplyStyle = true;

                AddHeader(
                    sheet,
                     L("ProductTypeName"),
                     L("Remark"),
                     L("IsValid"),
                     L("CreateBy"),
                     L("CreationTime"),
                     L("UpdateBy"),
                     L("UpdateTime")

                    );
                AddObjects(sheet, 2, productTypeListDtos,

             _ => _.ProductTypeName,

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
