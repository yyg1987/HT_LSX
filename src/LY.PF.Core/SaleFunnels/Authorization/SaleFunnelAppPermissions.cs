                          
namespace LY.PF.SaleFunnels.Authorization
{
	 /// <summary>
	 /// 定义系统的权限名称的字符串常量。
     /// <see cref="SaleFunnelAppAuthorizationProvider"/>中对权限的定义.
     /// </summary>
  public static   class SaleFunnelAppPermissions
    {
      

        /// <summary>
        /// 销售漏斗管理权限
        /// </summary>
        public const string SaleFunnel = "Pages.SaleFunnel";

	 

		/// <summary>
        /// 销售漏斗创建权限
        /// </summary>
        public const string SaleFunnel_CreateSaleFunnel = "Pages.SaleFunnel.CreateSaleFunnel";
		/// <summary>
        /// 销售漏斗修改权限
        /// </summary>
        public const string SaleFunnel_EditSaleFunnel = "Pages.SaleFunnel.EditSaleFunnel";
		/// <summary>
        /// 销售漏斗删除权限
        /// </summary>
        public const string SaleFunnel_DeleteSaleFunnel = "Pages.SaleFunnel.DeleteSaleFunnel";
    }
	
}

