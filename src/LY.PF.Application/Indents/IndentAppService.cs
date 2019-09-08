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
using LY.PF.Indents.Authorization;
using LY.PF.Indents.Dtos;
using LY.PF.Dto;
using Abp.Extensions;
using LY.PF.ProductTypes;

namespace LY.PF.Indents
{
    /// <summary>
    /// 订单服务实现
    /// </summary>
    [AbpAuthorize(IndentAppPermissions.Indent)]


    public class IndentAppService : PFAppServiceBase, IIndentAppService
    {
        private readonly IRepository<Indent, System.Guid> _indentRepository;
        private readonly IIndentListExcelExporter _indentListExcelExporter;
        private readonly IRepository<ProductType, int> _productTypeRepository;


        private readonly IndentManage _indentManage;
        /// <summary>
        /// 构造方法
        /// </summary>
        public IndentAppService(IRepository<Indent, System.Guid> indentRepository,
            IndentManage indentManage,
            IRepository<ProductType, int> productTypeRepository,
            IIndentListExcelExporter indentListExcelExporter
            )
        {
            _indentRepository = indentRepository;
            _indentManage = indentManage;
            _productTypeRepository = productTypeRepository;
            _indentListExcelExporter = indentListExcelExporter;
        }

        #region 实体的自定义扩展方法
        private IQueryable<Indent> _indentRepositoryAsNoTrack => _indentRepository.GetAll().AsNoTracking();


        #endregion

        #region 订单管理

        /// <summary>
        /// 根据查询条件获取订单分页列表
        /// </summary>
        public async Task<PagedResultDto<IndentListDto>> GetPagedIndentsAsync(GetIndentInput input)
        {

            var query = _indentRepositoryAsNoTrack;
            //TODO:根据传入的参数添加过滤条件
            query = query
                .WhereIf(!input.FilterText.IsNullOrWhiteSpace(), item => item.ProductName.Contains(input.FilterText) || item.Remark.Contains(input.FilterText));

            var indentCount = await query.CountAsync();

            var indents = await query
            .OrderBy(input.Sorting)
            .PageBy(input)
            .ToListAsync();

            var indentListDtos = indents.MapTo<List<IndentListDto>>();
            return new PagedResultDto<IndentListDto>(
            indentCount,
            indentListDtos
            );
        }

        /// <summary>
        /// 通过Id获取订单信息进行编辑或修改 
        /// </summary>
        public async Task<GetIndentForEditOutput> GetIndentForEditAsync(NullableIdDto<System.Guid> input)
        {
            var output = new GetIndentForEditOutput();

            IndentEditDto indentEditDto;

            if (input.Id.HasValue)
            {
                var entity = await _indentRepository.GetAsync(input.Id.Value);
                indentEditDto = entity.MapTo<IndentEditDto>();
            }
            else
            {
                indentEditDto = new IndentEditDto();
            }

            var all = await _productTypeRepository.GetAllListAsync();
            var allList = new List<ComboboxItemDto>();
            allList.Add(new ComboboxItemDto() { Value = "0", DisplayText = "请选择" });
            foreach (var item in all)
            {
                allList.Add(new ComboboxItemDto() { Value = item.Id.ToString(), DisplayText = item.ProductTypeName });
            }
            output.ProductTypes = allList;

            output.Indent = indentEditDto;
            return output;
        }


        /// <summary>
        /// 通过指定id获取订单ListDto信息
        /// </summary>
        public async Task<IndentListDto> GetIndentByIdAsync(EntityDto<System.Guid> input)
        {
            var entity = await _indentRepository.GetAsync(input.Id);

            return entity.MapTo<IndentListDto>();
        }

        /// <summary>
        /// 新增或更改订单
        /// </summary>
        public async Task CreateOrUpdateIndentAsync(CreateOrUpdateIndentInput input)
        {
            if (input.IndentEditDto.Id.HasValue)
            {
                await UpdateIndentAsync(input.IndentEditDto);
            }
            else
            {
                await CreateIndentAsync(input.IndentEditDto);
            }
        }

        /// <summary>
        /// 新增订单
        /// </summary>
        [AbpAuthorize(IndentAppPermissions.Indent_CreateIndent)]
        public virtual async Task<IndentEditDto> CreateIndentAsync(IndentEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增

            var entity = input.MapTo<Indent>();
            entity.CreateTime = DateTime.Now;
            entity.Id =  Guid.NewGuid();
            entity.CreateBy = GetCurrentUser().UserName;
            entity.UpdateTime = DateTime.Now;
            entity = await _indentRepository.InsertAsync(entity);
            return entity.MapTo<IndentEditDto>();
        }

        /// <summary>
        /// 编辑订单
        /// </summary>
        [AbpAuthorize(IndentAppPermissions.Indent_EditIndent)]
        public virtual async Task UpdateIndentAsync(IndentEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新

            var entity = await _indentRepository.GetAsync(input.Id.Value);
            input.MapTo(entity);
            entity.UpdateTime = DateTime.Now;
            entity.UpdateBy = GetCurrentUser().UserName;

            await _indentRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// 删除订单
        /// </summary>
        [AbpAuthorize(IndentAppPermissions.Indent_DeleteIndent)]
        public async Task DeleteIndentAsync(EntityDto<System.Guid> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            await _indentRepository.DeleteAsync(input.Id);
        }

        /// <summary>
        /// 批量删除订单
        /// </summary>
        [AbpAuthorize(IndentAppPermissions.Indent_DeleteIndent)]
        public async Task BatchDeleteIndentAsync(List<System.Guid> input)
        {
            //TODO:批量删除前的逻辑判断，是否允许删除
            await _indentRepository.DeleteAsync(s => input.Contains(s.Id));
        }

        #endregion
        #region 订单的Excel导出功能


        public async Task<FileDto> GetIndentToExcel()
        {
            var entities = await _indentRepository.GetAll().ToListAsync();

            var dtos = entities.MapTo<List<IndentListDto>>();

            var fileDto = _indentListExcelExporter.ExportIndentToFile(dtos);



            return fileDto;
        }


        #endregion
    }
}
