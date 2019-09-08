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
using LY.PF.SaleOrders.Authorization;
using LY.PF.SaleOrders.Dtos;
using LY.PF.Dto;
using LY.PF.ProductTypes;
using System.Web;
using System.IO;
using Abp.Extensions;

namespace LY.PF.SaleOrders
{
    /// <summary>
    /// 订单服务实现
    /// </summary>
    [AbpAuthorize(SaleOrderAppPermissions.SaleOrder)]


    public class SaleOrderAppService : PFAppServiceBase, ISaleOrderAppService
    {
        private readonly IRepository<SaleOrder, System.Guid> _saleOrderRepository;
        private readonly ISaleOrderListExcelExporter _saleOrderListExcelExporter;
        private readonly IRepository<ProductType, int> _productTypeRepository;


        private readonly SaleOrderManage _saleOrderManage;
        /// <summary>
        /// 构造方法
        /// </summary>
        public SaleOrderAppService(IRepository<SaleOrder, System.Guid> saleOrderRepository,
        SaleOrderManage saleOrderManage,
        IRepository<ProductType, int> productTypeRepository,
        ISaleOrderListExcelExporter saleOrderListExcelExporter
  )
        {
            _saleOrderRepository = saleOrderRepository;
            _saleOrderManage = saleOrderManage;
            _productTypeRepository = productTypeRepository;
            _saleOrderListExcelExporter = saleOrderListExcelExporter;
        }


        #region 实体的自定义扩展方法
        private IQueryable<SaleOrder> _saleOrderRepositoryAsNoTrack => _saleOrderRepository.GetAll().AsNoTracking();


        #endregion


        #region 订单管理

        /// <summary>
        /// 根据查询条件获取订单分页列表
        /// </summary>
        public async Task<PagedResultDto<SaleOrderListDto>> GetPagedSaleOrdersAsync(GetSaleOrderInput input)
        {

            var query = _saleOrderRepositoryAsNoTrack;
            //TODO:根据传入的参数添加过滤条件
            query = query
                .WhereIf(!input.FilterText.IsNullOrWhiteSpace(), item => item.ProductName.Contains(input.FilterText) || item.Remark.Contains(input.FilterText));

            var saleOrderCount = await query.CountAsync();

            var saleOrders = await query
            .OrderBy(input.Sorting)
            .PageBy(input)
            .ToListAsync();

            var saleOrderListDtos = saleOrders.MapTo<List<SaleOrderListDto>>();
            foreach (var item in saleOrderListDtos)
            {
               item.ProductTypeName=_productTypeRepository.FirstOrDefault(item.ProductType) == null?"": _productTypeRepository.FirstOrDefault(item.ProductType).ProductTypeName;
                if (item.Status == 0)
                {
                    item.StatusName = "正常";
                }
                else
                {
                    item.StatusName = "禁用";
                }
            }
            return new PagedResultDto<SaleOrderListDto>(
            saleOrderCount,
            saleOrderListDtos
            );
        }

        /// <summary>
        /// 通过Id获取订单信息进行编辑或修改 
        /// </summary>
        public async Task<GetSaleOrderForEditOutput> GetSaleOrderForEditAsync(NullableIdDto<System.Guid> input)
        {
            var output = new GetSaleOrderForEditOutput();

            SaleOrderEditDto saleOrderEditDto;

            if (input.Id.HasValue)
            {
                var entity = await _saleOrderRepository.GetAsync(input.Id.Value);
                saleOrderEditDto = entity.MapTo<SaleOrderEditDto>();
            }
            else
            {
                saleOrderEditDto = new SaleOrderEditDto();
            }

            var all = await _productTypeRepository.GetAllListAsync();
            var allList = new List<ComboboxItemDto>();
            allList.Add(new ComboboxItemDto() { Value = "0", DisplayText = "请选择" });
            foreach (var item in all)
            {
                allList.Add(new ComboboxItemDto() { Value = item.Id.ToString(), DisplayText = item.ProductTypeName });
            }
            output.ProductTypes = allList;

            output.SaleOrder = saleOrderEditDto;
            return output;
        }


        /// <summary>
        /// 通过指定id获取订单ListDto信息
        /// </summary>
        public async Task<SaleOrderListDto> GetSaleOrderByIdAsync(EntityDto<System.Guid> input)
        {
            var entity = await _saleOrderRepository.GetAsync(input.Id);

            return entity.MapTo<SaleOrderListDto>();
        }

        /// <summary>
        /// 新增或更改订单
        /// </summary>
        public async Task CreateOrUpdateSaleOrderAsync(CreateOrUpdateSaleOrderInput input)
        {

            //    HttpPostedFileBase item =  HttpRequest as HttpPostedFileBase;

            if (input.SaleOrderEditDto.Id.HasValue)
            {
                await UpdateSaleOrderAsync(input.SaleOrderEditDto);
            }
            else
            {
                await CreateSaleOrderAsync(input.SaleOrderEditDto);
            }
        }

        /// <summary>
        /// 新增订单
        /// </summary>
        [AbpAuthorize(SaleOrderAppPermissions.SaleOrder_CreateSaleOrder)]
        public virtual async Task<SaleOrderEditDto> CreateSaleOrderAsync(SaleOrderEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增

            var entity = input.MapTo<SaleOrder>();
            entity.CreateTime = DateTime.Now;
            entity.Id = Guid.NewGuid(); ///new Guid();
            entity.CreateBy = GetCurrentUser().UserName;
            entity.UpdateBy = GetCurrentUser().UserName;
            entity.UpdateTime = DateTime.Now;
            entity = await _saleOrderRepository.InsertAsync(entity);
            return entity.MapTo<SaleOrderEditDto>();
        }

        /// <summary>
        /// 编辑订单
        /// </summary>
        [AbpAuthorize(SaleOrderAppPermissions.SaleOrder_EditSaleOrder)]
        public virtual async Task UpdateSaleOrderAsync(SaleOrderEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新
            var entity = await _saleOrderRepository.GetAsync(input.Id.Value);
            // input.MapTo(entity);
            entity.ProductName = input.ProductName;
            entity.ProductType = input.ProductType;
            entity.ProductModel = input.ProductModel;
            entity.Status = input.Status;
            entity.Remark = input.Remark;

            entity.UpdateTime = DateTime.Now;
            entity.UpdateBy = GetCurrentUser().UserName;
            await _saleOrderRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// 删除订单
        /// </summary>
        [AbpAuthorize(SaleOrderAppPermissions.SaleOrder_DeleteSaleOrder)]
        public async Task DeleteSaleOrderAsync(EntityDto<System.Guid> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            await _saleOrderRepository.DeleteAsync(input.Id);
        }

        /// <summary>
        /// 批量删除订单
        /// </summary>
        [AbpAuthorize(SaleOrderAppPermissions.SaleOrder_DeleteSaleOrder)]
        public async Task BatchDeleteSaleOrderAsync(List<System.Guid> input)
        {
            //TODO:批量删除前的逻辑判断，是否允许删除
            await _saleOrderRepository.DeleteAsync(s => input.Contains(s.Id));
        }

         public bool Add(int productType, string productName, string remark, int status, string fileName, string authorityPart)
        {

            //TODO : 验证companyCode合法性
            if (_productTypeRepository.GetAll().Where(s => s.Id == productType).FirstOrDefault() == null)
            {
                return false;
            }
            //所有商户
            var relativePath = Path.Combine(AppConsts.Dir.Trim('~', '/'), fileName);
            var downUrl = Path.Combine(authorityPart, relativePath);        //绝对路径
            string canonicalUrl = downUrl.Replace('\\', '/');
            var version = Path.GetFileNameWithoutExtension(fileName);

                        var saleOrder = new SaleOrder
                        {
                            Id=Guid.NewGuid(),
                            ProductName = productName,
                            ProductType = productType,
                            Status = status,
                            Remark = remark,
                            ImageUrl = canonicalUrl,
                            CreateBy = GetCurrentUser().UserName,
                            CreateTime = DateTime.Now,
                        };
                        this._saleOrderRepository.Insert(saleOrder);
            return true;
        }


        #endregion
        #region 订单的Excel导出功能


        public async Task<FileDto> GetSaleOrderToExcel()
        {
            var entities = await _saleOrderRepository.GetAll().ToListAsync();

            var dtos = entities.MapTo<List<SaleOrderListDto>>();

            var fileDto = _saleOrderListExcelExporter.ExportSaleOrderToFile(dtos);



            return fileDto;
        }


        #endregion
    }
}
