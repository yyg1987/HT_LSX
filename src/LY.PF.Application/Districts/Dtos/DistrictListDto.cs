using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
namespace LY.PF.Districts.Dtos
{
    /// <summary>
    /// 订单列表Dto
    /// </summary>
    [AutoMapFrom(typeof(District))]
    public class DistrictListDto : EntityDto<int>
    {
        public string DistrictName { get; set; }
        public string Address { get; set; }
        public string Remark { get; set; }
        public bool IsValid { get; set; }
        public int ParentDistrictId { get; set; }
        public string CreateBy { get; set; }
        public string UpdateBy { get; set; }
        public DateTime UpdateTime { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
