using System;
using System.ComponentModel;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
namespace LY.PF.SaleFunnels.Dtos
{
    /// <summary>
    /// 销售漏斗列表Dto
    /// </summary>
    [AutoMapFrom(typeof(SaleFunnel))]
    public class SaleFunnelListDto : EntityDto<System.Guid>
    {
        public int ClientType { get; set; }
        public string ClientTypeName { get; set; }
        public int ProductType { get; set; }
        public string ProductTypeName { get; set; }
        public int Number { get; set; }
        public DateTime StatementTime { get; set; }
        public int ContendNumber { get; set; }
        public DateTime StageTime { get; set; }
        public DateTime LastTime { get; set; }
        public DateTime NextTime { get; set; }
        public int District { get; set; }
        public string DistrictName { get; set; }

        public string Saler { get; set; }
        public string ClientName { get; set; }
        public string Adress { get; set; }
        public string ProductName { get; set; }
        public string ProductModel { get; set; }
        public decimal Price { get; set; }
        public decimal SumPrice { get; set; }
        public string Opportunitie { get; set; }
        public string Stage { get; set; }
        public string ChanceSum { get; set; }
        public string NextAction { get; set; }
        public string RivalA { get; set; }
        public string ProductModelA { get; set; }
        public string RivalB { get; set; }
        public string ProductModelB { get; set; }
        public string Contact { get; set; }
        public string ContactMobile { get; set; }
        public string Purchase { get; set; }
        public string PurchaseMobile { get; set; }
        public string Dean { get; set; }
        public string DeanMobile { get; set; }
        public string LeadSource { get; set; }
        public string CreateBy { get; set; }
        public string UpdateBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        public DateTime CreationTime { get; set; }
        public DateTime? UpdateTime { get; set; }
    }
}
