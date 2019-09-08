namespace LY.PF.ClientTypes.Authorization
{
	 /// <summary>
	 /// 定义系统的权限名称的字符串常量。
     /// <see cref="ClientTypeAppAuthorizationProvider"/>中对权限的定义.
     /// </summary>
  public static   class ClientTypeAppPermissions
    {
      

        /// <summary>
        /// 销售漏斗管理权限
        /// </summary>
        public const string ClientType = "Pages.ClientType";

	 

		/// <summary>
        /// 销售漏斗创建权限
        /// </summary>
        public const string ClientType_CreateClientType = "Pages.ClientType.CreateClientType";
		/// <summary>
        /// 销售漏斗修改权限
        /// </summary>
        public const string ClientType_EditClientType = "Pages.ClientType.EditClientType";
		/// <summary>
        /// 销售漏斗删除权限
        /// </summary>
        public const string ClientType_DeleteClientType = "Pages.ClientType.DeleteClientType";
    }
	
}

