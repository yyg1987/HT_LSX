using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
namespace LY.PF.SaleOrders.Dtos
{
    /// <summary>
    /// 订单列表Dto
    /// </summary>
    [AutoMapFrom(typeof(SaleOrder))]
    public class SaleOrderListDto : EntityDto<System.Guid>
    {
        public int ProductType { get; set; }
        public string ProductTypeName { get; set; }
        public string ProductName { get; set; }
        public string ProductModel { get; set; }
        public string ProductBrand { get; set; }
        public string Remark { get; set; }
        public int ScheduleNumber { get; set; }
        public string ImageUrl { get; set; }
        public int ActualNumber { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
        public int Status { get; set; }
        public string StatusName { get; set; }

        public string CreateBy { get; set; }
        public string UpdateBy { get; set; }

        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
