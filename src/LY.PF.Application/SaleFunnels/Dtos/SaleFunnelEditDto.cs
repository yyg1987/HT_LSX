using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
namespace LY.PF.SaleFunnels.Dtos
{
    /// <summary>
    /// 销售漏斗编辑用Dto
    /// </summary>
    [AutoMap(typeof(SaleFunnel))]
    public class SaleFunnelEditDto 
    {

	/// <summary>
    ///   主键Id
    /// </summary>
    [DisplayName("主键Id")]
	public System.Guid? Id{get;set;}

        [MaxLength(64)]
        public   string  District { get; set; }

        [MaxLength(64)]
        public   string  Saler { get; set; }

        [MaxLength(64)]
        public   string  ClientName { get; set; }

        [MaxLength(512)]
        public   string  Adress { get; set; }

        public   int  ClientType { get; set; }

        public   int  ProductType { get; set; }

        [MaxLength(64)]
        public   string  ProductName { get; set; }

        [MaxLength(64)]
        public   string  ProductModel { get; set; }

        public   decimal  Price { get; set; }

        public   int  Number { get; set; }

        public   decimal  SumPrice { get; set; }

        public   DateTime  StatementTime { get; set; }

        public   int  ContendNumber { get; set; }

        [MaxLength(64)]
        public   string  Opportunitie { get; set; }

        [MaxLength(64)]
        public   string  Stage { get; set; }

        public   DateTime  StageTime { get; set; }

        [MaxLength(64)]
        public   string  ChanceSum { get; set; }

        public   DateTime  LastTime { get; set; }

        [MaxLength(512)]
        public   string  NextAction { get; set; }

        public   DateTime  NextTime { get; set; }

        [MaxLength(64)]
        public   string  RivalA { get; set; }

        [MaxLength(64)]
        public   string  ProductModelA { get; set; }

        [MaxLength(64)]
        public   string  RivalB { get; set; }

        [MaxLength(64)]
        public   string  ProductModelB { get; set; }

        [MaxLength(64)]
        public   string  Contact { get; set; }

        [MaxLength(64)]
        public   string  ContactMobile { get; set; }

        [MaxLength(64)]
        public   string  Purchase { get; set; }

        [MaxLength(64)]
        public   string  PurchaseMobile { get; set; }

        [MaxLength(64)]
        public   string  Dean { get; set; }

        [MaxLength(64)]
        public   string  DeanMobile { get; set; }

        [MaxLength(64)]
        public   string  LeadSource { get; set; }

        [MaxLength(32)]
        public   string  CreateBy { get; set; }

        public   DateTime?  UpdateTime { get; set; }

        [MaxLength(32)]
        public   string  UpdateBy { get; set; }

    }
}
