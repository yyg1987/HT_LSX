using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
namespace LY.PF.ProductTypes.Dtos
{
    /// <summary>
    /// 订单编辑用Dto
    /// </summary>
    [AutoMap(typeof(ProductType))]
    public class ProductTypeEditDto
    {

        /// <summary>
        ///   主键Id
        /// </summary>
        [DisplayName("主键Id")]
        public int? Id { get; set; }

        [MaxLength(64)]
        public string ProductTypeName { get; set; }

        public bool IsValid { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        [MaxLength(512)]
        public string Remark { get; set; }

        [MaxLength(32)]
        public string CreateBy { get; set; }

        public DateTime CreationTime { get; set; }

        [MaxLength(32)]
        public string UpdateBy { get; set; }

        public DateTime UpdateTime { get; set; }

    }
}
