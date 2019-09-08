using Abp.Domain.Entities;
using System;
namespace LY.PF.Indents
{
    public partial class Indent : Entity<Guid>
    {
        public string ProductName { get; set; }
        public int ProductType { get; set; }
        public string ProductModel { get; set; }
        public string ProductBrand { get; set; }
        public int ScheduleNumber { get; set; }
        public string ImageUrl { get; set; }
        public int ActualNumber { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
        public int Status { get; set; }
        public string Remark { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateTime { get; set; }
        public string UpdateBy { get; set; }
        public DateTime UpdateTime { get; set; }
    }

}
