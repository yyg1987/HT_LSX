using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
namespace LY.PF.Indents.Dtos
{
    /// <summary>
    /// 订单列表Dto
    /// </summary>
    [AutoMapFrom(typeof(Indent))]
    public class IndentListDto : EntityDto<System.Guid>
    {
        public string ProductName { get; set; }
        public int ProductType { get; set; }
        public int ScheduleNumber { get; set; }
        public int ActualNumber { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
        public int Status { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
