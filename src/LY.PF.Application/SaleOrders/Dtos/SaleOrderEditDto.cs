using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
namespace LY.PF.SaleOrders.Dtos
{
    /// <summary>
    /// 订单编辑用Dto
    /// </summary>
    [AutoMap(typeof(SaleOrder))]
    public class SaleOrderEditDto
    {

        /// <summary>
        ///   主键Id
        /// </summary>
        [DisplayName("主键Id")]
        public System.Guid? Id { get; set; }

        [MaxLength(64)]
        public string ProductName { get; set; }

        public int ProductType { get; set; }
        public string ProductModel { get; set; }
        public string ProductBrand { get; set; }

        public int ScheduleNumber { get; set; }

        [MaxLength(512)]
        public string ImageUrl { get; set; }

        public int ActualNumber { get; set; }

        public decimal Price { get; set; }

        public decimal TotalPrice { get; set; }

        public int Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        [MaxLength(512)]
        public string Remark { get; set; }

        [MaxLength(32)]
        public string CreateBy { get; set; }

        public DateTime CreateTime { get; set; }

        [MaxLength(32)]
        public string UpdateBy { get; set; }

        public DateTime UpdateTime { get; set; }

    }
}
