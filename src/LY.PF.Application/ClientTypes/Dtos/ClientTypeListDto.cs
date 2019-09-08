using System;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
namespace LY.PF.ClientTypes.Dtos
{
    /// <summary>
    /// 订单列表Dto
    /// </summary>
    [AutoMapFrom(typeof(ClientType))]
    public class ClientTypeListDto : EntityDto<int>
    {
        public string ClientTypeName { get; set; }
        public string Remark { get; set; }
        public bool IsValid { get; set; }
        public string CreateBy { get; set; }
        public string UpdateBy { get; set; }
        public DateTime UpdateTime { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
