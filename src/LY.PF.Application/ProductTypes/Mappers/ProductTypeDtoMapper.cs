using AutoMapper;
namespace LY.PF.ProductTypes.Mappers
{
    /// <summary>
    /// ProductTypeDto映射配置
    /// </summary>
    public class ProductTypeDtoMapper
    {

        private static volatile bool _mappedBefore;
        private static readonly object SyncObj = new object();
        /// <summary>
        /// 初始化映射
        /// </summary>
        public void CreateMappings(IMapperConfigurationExpression configuration)
        {

            lock (SyncObj)
            {
                if (_mappedBefore)
                {
                    return;
                }

                CreateMappingsInternal(configuration);

                _mappedBefore = true;
            }

        }

        /// <summary>
        ///    Configuration.Modules.AbpAutoMapper().Configurators.Add(ProductTypeDtoMapper.CreateMappings);
        ///注入位置    < see cref = "AbpZeroTemplateApplicationModule" /> 
        /// <param name="configuration"></param>
        /// </summary>       
        private static void CreateMappingsInternal(IMapperConfigurationExpression configuration)
        {

            //默认ABP功能已经实现了，如果你要单独对DTO进行拓展，可以在此处放开注释文件。

            // Configuration.Modules.AbpAutoMapper().Configurators.Add(ProductTypeDtoMapper.CreateMappings);

            //    Mapper.CreateMap<ProductType,ProductTypeEditDto>();
            //     Mapper.CreateMap<ProductType, ProductTypeListDto>();

            //       Mapper.CreateMap<ProductTypeEditDto, ProductType>();
            //        Mapper.CreateMap<ProductTypeListDto,ProductType>();

        }

    }
}