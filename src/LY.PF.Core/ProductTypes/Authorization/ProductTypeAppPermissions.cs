namespace LY.PF.ProductTypes.Authorization
{
	 /// <summary>
	 /// 定义系统的权限名称的字符串常量。
     /// <see cref="ProductTypeAppAuthorizationProvider"/>中对权限的定义.
     /// </summary>
  public static   class ProductTypeAppPermissions
    {
      

        /// <summary>
        /// 销售漏斗管理权限
        /// </summary>
        public const string ProductType = "Pages.ProductType";

	 

		/// <summary>
        /// 销售漏斗创建权限
        /// </summary>
        public const string ProductType_CreateProductType = "Pages.ProductType.CreateProductType";
		/// <summary>
        /// 销售漏斗修改权限
        /// </summary>
        public const string ProductType_EditProductType = "Pages.ProductType.EditProductType";
		/// <summary>
        /// 销售漏斗删除权限
        /// </summary>
        public const string ProductType_DeleteProductType = "Pages.ProductType.DeleteProductType";
    }
	
}

