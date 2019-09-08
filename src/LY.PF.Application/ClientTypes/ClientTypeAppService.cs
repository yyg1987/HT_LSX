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
using LY.PF.ClientTypes.Authorization;
using LY.PF.ClientTypes.Dtos;
using LY.PF.Dto;

namespace LY.PF.ClientTypes
{
    /// <summary>
    /// 订单服务实现
    /// </summary>
    [AbpAuthorize(ClientTypeAppPermissions.ClientType)]


    public class ClientTypeAppService : PFAppServiceBase, IClientTypeAppService
    {
        private readonly IRepository<ClientType, int> _clientTypeRepository;
        private readonly IClientTypeListExcelExporter _clientTypeListExcelExporter;


        private readonly ClientTypeManage _clientTypeManage;
        /// <summary>
        /// 构造方法
        /// </summary>
        public ClientTypeAppService(IRepository<ClientType, int> clientTypeRepository,
ClientTypeManage clientTypeManage
      , IClientTypeListExcelExporter clientTypeListExcelExporter
  )
        {
            _clientTypeRepository = clientTypeRepository;
            _clientTypeManage = clientTypeManage;
            _clientTypeListExcelExporter = clientTypeListExcelExporter;
        }


        #region 实体的自定义扩展方法
        private IQueryable<ClientType> _clientTypeRepositoryAsNoTrack => _clientTypeRepository.GetAll().AsNoTracking();


        #endregion


        #region 订单管理

        /// <summary>
        /// 根据查询条件获取订单分页列表
        /// </summary>
        public async Task<PagedResultDto<ClientTypeListDto>> GetPagedClientTypesAsync(GetClientTypeInput input)
        {

            var query = _clientTypeRepositoryAsNoTrack;
            //TODO:根据传入的参数添加过滤条件

            var clientTypeCount = await query.CountAsync();

            var clientTypes = await query
            .OrderBy(input.Sorting)
            .PageBy(input)
            .ToListAsync();

            var clientTypeListDtos = clientTypes.MapTo<List<ClientTypeListDto>>();
            return new PagedResultDto<ClientTypeListDto>(
            clientTypeCount,
            clientTypeListDtos
            );
        }

        /// <summary>
        /// 通过Id获取订单信息进行编辑或修改 
        /// </summary>
        public async Task<GetClientTypeForEditOutput> GetClientTypeForEditAsync(NullableIdDto<System.Int32> input)
        {
            var output = new GetClientTypeForEditOutput();

            ClientTypeEditDto clientTypeEditDto;

            if (input.Id.HasValue)
            {
                var entity = await _clientTypeRepository.GetAsync(input.Id.Value);
                clientTypeEditDto = entity.MapTo<ClientTypeEditDto>();
            }
            else
            {
                clientTypeEditDto = new ClientTypeEditDto();
            }

            output.ClientType = clientTypeEditDto;
            return output;
        }


        /// <summary>
        /// 通过指定id获取订单ListDto信息
        /// </summary>
        public async Task<ClientTypeListDto> GetClientTypeByIdAsync(EntityDto<System.Int32> input)
        {
            var entity = await _clientTypeRepository.GetAsync(input.Id);

            return entity.MapTo<ClientTypeListDto>();
        }

        /// <summary>
        /// 新增或更改订单
        /// </summary>
        public async Task CreateOrUpdateClientTypeAsync(CreateOrUpdateClientTypeInput input)
        {
            if (input.ClientTypeEditDto.Id.HasValue)
            {
                await UpdateClientTypeAsync(input.ClientTypeEditDto);
            }
            else
            {
                await CreateClientTypeAsync(input.ClientTypeEditDto);
            }
        }

        /// <summary>
        /// 新增订单
        /// </summary>
        [AbpAuthorize(ClientTypeAppPermissions.ClientType_CreateClientType)]
        public virtual async Task<ClientTypeEditDto> CreateClientTypeAsync(ClientTypeEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增

            var entity = input.MapTo<ClientType>();
            entity.CreationTime = DateTime.Now;
            //entity.Id = new Guid();
            entity.CreateBy = GetCurrentUser().UserName;
            entity.UpdateTime = DateTime.Now;
            entity = await _clientTypeRepository.InsertAsync(entity);
            return entity.MapTo<ClientTypeEditDto>();
        }

        /// <summary>
        /// 编辑订单
        /// </summary>
        [AbpAuthorize(ClientTypeAppPermissions.ClientType_EditClientType)]
        public virtual async Task UpdateClientTypeAsync(ClientTypeEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新
            
            var entity = await _clientTypeRepository.GetAsync(input.Id.Value);
            input.MapTo(entity);
            entity.UpdateTime = DateTime.Now;
            entity.UpdateBy = GetCurrentUser().UserName;

            await _clientTypeRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// 删除订单
        /// </summary>
        [AbpAuthorize(ClientTypeAppPermissions.ClientType_DeleteClientType)]
        public async Task DeleteClientTypeAsync(EntityDto<int> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            await _clientTypeRepository.DeleteAsync(input.Id);
        }

        /// <summary>
        /// 批量删除订单
        /// </summary>
        [AbpAuthorize(ClientTypeAppPermissions.ClientType_DeleteClientType)]
        public async Task BatchDeleteClientTypeAsync(List<int> input)
        {
            //TODO:批量删除前的逻辑判断，是否允许删除
            await _clientTypeRepository.DeleteAsync(s => input.Contains(s.Id));
        }

        #endregion
        #region 订单的Excel导出功能


        public async Task<FileDto> GetClientTypeToExcel()
        {
            var entities = await _clientTypeRepository.GetAll().ToListAsync();

            var dtos = entities.MapTo<List<ClientTypeListDto>>();

            var fileDto = _clientTypeListExcelExporter.ExportClientTypeToFile(dtos);



            return fileDto;
        }


        #endregion
    }
}
