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
using Abp.Extensions;
using Abp.Linq.Extensions;
using LY.PF.Districts.Authorization;
using LY.PF.Districts.Dtos;
using LY.PF.Dto;

namespace LY.PF.Districts
{
    /// <summary>
    /// 订单服务实现
    /// </summary>
    [AbpAuthorize(DistrictAppPermissions.District)]


    public class DistrictAppService : PFAppServiceBase, IDistrictAppService
    {
        private readonly IRepository<District,int> _districtRepository;
        private readonly IDistrictListExcelExporter _districtListExcelExporter;


        private readonly DistrictManage _districtManage;
        /// <summary>
        /// 构造方法
        /// </summary>
        public DistrictAppService(IRepository<District, int> districtRepository,
DistrictManage districtManage
      , IDistrictListExcelExporter districtListExcelExporter
  )
        {
            _districtRepository = districtRepository;
            _districtManage = districtManage;
            _districtListExcelExporter = districtListExcelExporter;
        }


        #region 实体的自定义扩展方法
        private IQueryable<District> _districtRepositoryAsNoTrack => _districtRepository.GetAll().AsNoTracking();


        #endregion


        #region 订单管理

        /// <summary>
        /// 根据查询条件获取订单分页列表
        /// </summary>
        public async Task<PagedResultDto<DistrictListDto>> GetPagedDistrictsAsync(GetDistrictInput input)
        {

            var query = _districtRepositoryAsNoTrack;
            //TODO:根据传入的参数添加过滤条件
            query = query
                .WhereIf(!input.FilterText.IsNullOrWhiteSpace(), item => item.DistrictName.Contains(input.FilterText) || item.Remark.Contains(input.FilterText));

            var districtCount = await query.CountAsync();

            var districts = await query
            .OrderBy(input.Sorting)
            .PageBy(input)
            .ToListAsync();

            var districtListDtos = districts.MapTo<List<DistrictListDto>>();
            return new PagedResultDto<DistrictListDto>(
            districtCount,
            districtListDtos
            );
        }

        /// <summary>
        /// 通过Id获取订单信息进行编辑或修改 
        /// </summary>
        public async Task<GetDistrictForEditOutput> GetDistrictForEditAsync(NullableIdDto<int> input)
        {
            var output = new GetDistrictForEditOutput();

            DistrictEditDto districtEditDto;

            if (input.Id.HasValue)
            {
                var entity = await _districtRepository.GetAsync(input.Id.Value);
                districtEditDto = entity.MapTo<DistrictEditDto>();
            }
            else
            {
                districtEditDto = new DistrictEditDto();
            }
            //母区域列表
            var all = await _districtRepository.GetAllListAsync();
            var allList = new List<ComboboxItemDto>();
            allList.Add(new ComboboxItemDto() { Value = "0", DisplayText = "请选择" });
            foreach (var item in all)
            {
                allList.Add(new ComboboxItemDto() { Value = item.Id.ToString(), DisplayText = item.DistrictName });
                SetDistrictSelect(all, allList, item, "--");
            }
            output.ParentDistricts = allList;
            output.District = districtEditDto;
            return output;
        }


        /// <summary>
        /// 通过指定id获取订单ListDto信息
        /// </summary>
        public async Task<DistrictListDto> GetDistrictByIdAsync(EntityDto<int> input)
        {
            var entity = await _districtRepository.GetAsync(input.Id);

            return entity.MapTo<DistrictListDto>();
        }

        /// <summary>
        /// 新增或更改订单
        /// </summary>
        public async Task CreateOrUpdateDistrictAsync(CreateOrUpdateDistrictInput input)
        {
            if (input.DistrictEditDto.Id.HasValue)
            {
                await UpdateDistrictAsync(input.DistrictEditDto);
            }
            else
            {
                await CreateDistrictAsync(input.DistrictEditDto);
            }
        }

        /// <summary>
        /// 新增订单
        /// </summary>
        [AbpAuthorize(DistrictAppPermissions.District_CreateDistrict)]
        public virtual async Task<DistrictEditDto> CreateDistrictAsync(DistrictEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增

            var entity = input.MapTo<District>();
            entity.CreationTime = DateTime.Now;
            //entity.Id = new Guid();
            entity.CreateBy = GetCurrentUser().UserName;
            entity.UpdateTime = DateTime.Now;
            entity = await _districtRepository.InsertAsync(entity);
            return entity.MapTo<DistrictEditDto>();
        }

        /// <summary>
        /// 编辑订单
        /// </summary>
        [AbpAuthorize(DistrictAppPermissions.District_EditDistrict)]
        public virtual async Task UpdateDistrictAsync(DistrictEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新
            
            var entity = await _districtRepository.GetAsync(input.Id.Value);
            input.ParentDistrictId= entity.ParentDistrictId;
            input.MapTo(entity);
            //entity.ParentDistrictId = entity.ParentDistrictId;
            entity.UpdateTime = DateTime.Now;
            entity.UpdateBy = GetCurrentUser().UserName;

            await _districtRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// 删除订单
        /// </summary>
        [AbpAuthorize(DistrictAppPermissions.District_DeleteDistrict)]
        public async Task DeleteDistrictAsync(EntityDto<int> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            await _districtRepository.DeleteAsync(input.Id);
        }

        /// <summary>
        /// 批量删除订单
        /// </summary>
        [AbpAuthorize(DistrictAppPermissions.District_DeleteDistrict)]
        public async Task BatchDeleteDistrictAsync(List<int> input)
        {
            //TODO:批量删除前的逻辑判断，是否允许删除
            await _districtRepository.DeleteAsync(s => input.Contains(s.Id));
        }



        /// <summary>
        /// 遍历当前公司下所有公司
        /// </summary>
        /// <param name="list">所有公司</param>
        /// <param name="resultList">下拉菜单</param>
        /// <param name="company">当前公司</param>
        /// <param name="rankStr">间隔符</param>
        public void SetDistrictSelect(List<District> list, List<ComboboxItemDto> resultList, District district, string rankStr)
        {
            var childrenList = list.Where(s => s.ParentDistrictId == district.Id);
            foreach (var item in childrenList)
            {
                resultList.Add(new ComboboxItemDto() { Value = item.Id.ToString(), DisplayText = rankStr + item.DistrictName });
                SetDistrictSelect(list, resultList, item, "--" + rankStr);
            }
        }

        #endregion
        #region 订单的Excel导出功能


        public async Task<FileDto> GetDistrictToExcel()
        {
            var entities = await _districtRepository.GetAll().ToListAsync();

            var dtos = entities.MapTo<List<DistrictListDto>>();

            var fileDto = _districtListExcelExporter.ExportDistrictToFile(dtos);



            return fileDto;
        }


        #endregion
    }
}
