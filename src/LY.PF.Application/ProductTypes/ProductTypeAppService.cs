using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using LY.PF.ProductTypes.Authorization;
using LY.PF.ProductTypes.Dtos;
using LY.PF.Dto;
using Abp.Extensions;

namespace LY.PF.ProductTypes
{
    /// <summary>
    /// 订单服务实现
    /// </summary>
    [AbpAuthorize(ProductTypeAppPermissions.ProductType)]


    public class ProductTypeAppService : PFAppServiceBase, IProductTypeAppService
    {
        private readonly IRepository<ProductType, int> _productTypeRepository;
        private readonly IProductTypeListExcelExporter _productTypeListExcelExporter;


        private readonly ProductTypeManage _productTypeManage;
        /// <summary>
        /// 构造方法
        /// </summary>
        public ProductTypeAppService(IRepository<ProductType, int> productTypeRepository,
ProductTypeManage productTypeManage
      , IProductTypeListExcelExporter productTypeListExcelExporter
  )
        {
            _productTypeRepository = productTypeRepository;
            _productTypeManage = productTypeManage;
            _productTypeListExcelExporter = productTypeListExcelExporter;
        }


        #region 实体的自定义扩展方法
        private IQueryable<ProductType> _productTypeRepositoryAsNoTrack => _productTypeRepository.GetAll().AsNoTracking();


        #endregion


        #region 订单管理

        /// <summary>
        /// 根据查询条件获取订单分页列表
        /// </summary>
        public async Task<PagedResultDto<ProductTypeListDto>> GetPagedProductTypesAsync(GetProductTypeInput input)
        {

            var query = _productTypeRepositoryAsNoTrack;
            //TODO:根据传入的参数添加过滤条件
            query = query
                .WhereIf(!input.FilterText.IsNullOrWhiteSpace(), item => item.ProductTypeName.Contains(input.FilterText) || item.Remark.Contains(input.FilterText));

            var productTypeCount = await query.CountAsync();

            var productTypes = await query
            .OrderBy(input.Sorting)
            .PageBy(input)
            .ToListAsync();

            var productTypeListDtos = productTypes.MapTo<List<ProductTypeListDto>>();
            return new PagedResultDto<ProductTypeListDto>(
            productTypeCount,
            productTypeListDtos
            );
        }

        /// <summary>
        /// 通过Id获取订单信息进行编辑或修改 
        /// </summary>
        public async Task<GetProductTypeForEditOutput> GetProductTypeForEditAsync(NullableIdDto<int> input)
        {
            var output = new GetProductTypeForEditOutput();

            ProductTypeEditDto productTypeEditDto;

            if (input.Id.HasValue)
            {
                var entity = await _productTypeRepository.GetAsync(input.Id.Value);
                productTypeEditDto = entity.MapTo<ProductTypeEditDto>();
            }
            else
            {
                productTypeEditDto = new ProductTypeEditDto();
            }

            output.ProductType = productTypeEditDto;
            return output;
        }


        /// <summary>
        /// 通过指定id获取订单ListDto信息
        /// </summary>
        public async Task<ProductTypeListDto> GetProductTypeByIdAsync(EntityDto<int> input)
        {
            var entity = await _productTypeRepository.GetAsync(input.Id);

            return entity.MapTo<ProductTypeListDto>();
        }

        /// <summary>
        /// 新增或更改订单
        /// </summary>
        public async Task CreateOrUpdateProductTypeAsync(CreateOrUpdateProductTypeInput input)
        {
            if (input.ProductTypeEditDto.Id.HasValue)
            {
                await UpdateProductTypeAsync(input.ProductTypeEditDto);
            }
            else
            {
                await CreateProductTypeAsync(input.ProductTypeEditDto);
            }
        }

        /// <summary>
        /// 新增订单
        /// </summary>
        [AbpAuthorize(ProductTypeAppPermissions.ProductType_CreateProductType)]
        public virtual async Task<ProductTypeEditDto> CreateProductTypeAsync(ProductTypeEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增

            var entity = input.MapTo<ProductType>();
            entity.CreationTime = DateTime.Now;
            //entity.Id = new Guid();
            entity.CreateBy = GetCurrentUser().UserName;
            entity.UpdateTime = DateTime.Now;
            entity = await _productTypeRepository.InsertAsync(entity);
            return entity.MapTo<ProductTypeEditDto>();
        }

        /// <summary>
        /// 编辑订单
        /// </summary>
        [AbpAuthorize(ProductTypeAppPermissions.ProductType_EditProductType)]
        public virtual async Task UpdateProductTypeAsync(ProductTypeEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新
            
            var entity = await _productTypeRepository.GetAsync(input.Id.Value);
            input.MapTo(entity);
            entity.UpdateTime = DateTime.Now;
            entity.UpdateBy = GetCurrentUser().UserName;

            await _productTypeRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// 删除订单
        /// </summary>
        [AbpAuthorize(ProductTypeAppPermissions.ProductType_DeleteProductType)]
        public async Task DeleteProductTypeAsync(EntityDto<int> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            await _productTypeRepository.DeleteAsync(input.Id);
        }

        /// <summary>
        /// 批量删除订单
        /// </summary>
        [AbpAuthorize(ProductTypeAppPermissions.ProductType_DeleteProductType)]
        public async Task BatchDeleteProductTypeAsync(List<int> input)
        {
            //TODO:批量删除前的逻辑判断，是否允许删除
            await _productTypeRepository.DeleteAsync(s => input.Contains(s.Id));
        }

        public List<ComboboxItemDto> GetProductTypes()
        {
            var all = _productTypeRepository.GetAll().ToList();

            // var  parentCompanys = all.Where(s => s.Level < 2).OrderBy(c => c.CompanyCode).ToList();
            var allList = new List<ComboboxItemDto>();
            foreach (var item in all)
            {
                allList.Add(new ComboboxItemDto() { Value = item.Id.ToString(), DisplayText = item.ProductTypeName });
            }
            return allList;

        }


        #endregion
        #region 订单的Excel导出功能


        public async Task<FileDto> GetProductTypeToExcel()
        {
            var entities = await _productTypeRepository.GetAll().ToListAsync();

            var dtos = entities.MapTo<List<ProductTypeListDto>>();

            var fileDto = _productTypeListExcelExporter.ExportProductTypeToFile(dtos);



            return fileDto;
        }


        #endregion
    }
}
