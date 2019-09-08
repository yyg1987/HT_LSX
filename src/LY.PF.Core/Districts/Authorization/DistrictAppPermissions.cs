namespace LY.PF.Districts.Authorization
{
	 /// <summary>
	 /// 定义系统的权限名称的字符串常量。
     /// <see cref="DistrictAppAuthorizationProvider"/>中对权限的定义.
     /// </summary>
  public static   class DistrictAppPermissions
    {
      

        /// <summary>
        /// 销售漏斗管理权限
        /// </summary>
        public const string District = "Pages.District";

	 

		/// <summary>
        /// 销售漏斗创建权限
        /// </summary>
        public const string District_CreateDistrict = "Pages.District.CreateDistrict";
		/// <summary>
        /// 销售漏斗修改权限
        /// </summary>
        public const string District_EditDistrict = "Pages.District.EditDistrict";
		/// <summary>
        /// 销售漏斗删除权限
        /// </summary>
        public const string District_DeleteDistrict = "Pages.District.DeleteDistrict";
    }
	
}

