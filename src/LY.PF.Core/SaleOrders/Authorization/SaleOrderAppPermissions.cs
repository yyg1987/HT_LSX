namespace LY.PF.SaleOrders.Authorization
{
    /// <summary>
    /// 定义系统的权限名称的字符串常量。
    /// <see cref="SaleOrderAppAuthorizationProvider"/>中对权限的定义.
    /// </summary>
    public static class SaleOrderAppPermissions
    {


        /// <summary>
        /// 订单管理权限
        /// </summary>
        public const string SaleOrder = "Pages.SaleOrder";



        /// <summary>
        /// 订单创建权限
        /// </summary>
        public const string SaleOrder_CreateSaleOrder = "Pages.SaleOrder.CreateSaleOrder";
        /// <summary>
        /// 订单修改权限
        /// </summary>
        public const string SaleOrder_EditSaleOrder = "Pages.SaleOrder.EditSaleOrder";
        /// <summary>
        /// 订单删除权限
        /// </summary>
        public const string SaleOrder_DeleteSaleOrder = "Pages.SaleOrder.DeleteSaleOrder";
    }

}

