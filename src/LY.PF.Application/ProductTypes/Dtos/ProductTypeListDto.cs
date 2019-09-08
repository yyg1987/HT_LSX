using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
namespace LY.PF.ProductTypes.Dtos
{
    /// <summary>
    /// 订单列表Dto
    /// </summary>
    [AutoMapFrom(typeof(ProductType))]
    public class ProductTypeListDto : EntityDto<int>
    {
        public string ProductTypeName { get; set; }
        public string Remark { get; set; }
        public bool IsValid { get; set; }
        public string CreateBy { get; set; }
        public string UpdateBy { get; set; }
        public DateTime UpdateTime { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
