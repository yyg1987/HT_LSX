using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
namespace LY.PF.SaleFunnels
{
    public partial class SaleFunnel : Entity<Guid>, IHasCreationTime
    {
        public int District { get; set; }
        public string Saler { get; set; }
        public string ClientName { get; set; }
        public string Adress { get; set; }
        public int ClientType { get; set; }
        public int ProductType { get; set; }
        public string ProductName { get; set; }
        public string ProductModel { get; set; }
        public decimal Price { get; set; }
        public int Number { get; set; }
        public decimal SumPrice { get; set; }
        public DateTime StatementTime { get; set; }
        public int ContendNumber { get; set; }
        public string Opportunitie { get; set; }
        public string Stage { get; set; }
        public DateTime StageTime { get; set; }
        public string ChanceSum { get; set; }
        public DateTime LastTime { get; set; }
        public string NextAction { get; set; }
        public DateTime NextTime { get; set; }
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
        public DateTime CreationTime { get; set; }
        public string CreateBy { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string UpdateBy { get; set; }
    }

}
