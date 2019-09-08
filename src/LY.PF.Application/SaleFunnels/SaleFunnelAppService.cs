using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Threading.Tasks;
using Abp;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Configuration;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using LY.PF.SaleFunnels.Authorization;
using LY.PF.SaleFunnels.Dtos;
using LY.PF.Dto;
using System;
using LY.PF.ProductTypes;
using LY.PF.ClientTypes;
using LY.PF.Districts;

namespace LY.PF.SaleFunnels
{
    /// <summary>
    /// 销售漏斗服务实现
    /// </summary>
    [AbpAuthorize(SaleFunnelAppPermissions.SaleFunnel)]


    public class SaleFunnelAppService : PFAppServiceBase, ISaleFunnelAppService
    {
        private readonly IRepository<SaleFunnel, System.Guid> _saleFunnelRepository;
        private readonly IRepository<ClientType, int> _clientTypeRepository;
        private readonly IRepository<ProductType, int> _productTypeRepository;
        private readonly IRepository<District, int> _districtRepository;

        private readonly ISaleFunnelListExcelExporter _saleFunnelListExcelExporter;


        private readonly SaleFunnelManage _saleFunnelManage;
        /// <summary>
        /// 构造方法
        /// </summary>
        public SaleFunnelAppService(IRepository<SaleFunnel, System.Guid> saleFunnelRepository,
        SaleFunnelManage saleFunnelManage,
        IRepository<ClientType, int> clientTypesRepository,
        IRepository<ProductType, int> productTypeRepository,
        IRepository<District, int> districtRepository,
        ISaleFunnelListExcelExporter saleFunnelListExcelExporter
  )
        {
            _saleFunnelRepository = saleFunnelRepository;
            _saleFunnelManage = saleFunnelManage;
            _clientTypeRepository = clientTypesRepository;
            _productTypeRepository = productTypeRepository;
            _districtRepository = districtRepository;
            _saleFunnelListExcelExporter = saleFunnelListExcelExporter;
        }


        #region 实体的自定义扩展方法
        private IQueryable<SaleFunnel> _saleFunnelRepositoryAsNoTrack => _saleFunnelRepository.GetAll().AsNoTracking();


        #endregion


        #region 销售漏斗管理

        /// <summary>
        /// 根据查询条件获取销售漏斗分页列表
        /// </summary>
        public async Task<PagedResultDto<SaleFunnelListDto>> GetPagedSaleFunnelsAsync(GetSaleFunnelInput input)
        {

            var query = _saleFunnelRepositoryAsNoTrack;
            //TODO:根据传入的参数添加过滤条件
            query = query
            .WhereIf(!input.FilterText.IsNullOrWhiteSpace(), item => item.Saler.Contains(input.FilterText) || item.ProductName.Contains(input.FilterText));

            var saleFunnelCount = await query.CountAsync();

            var saleFunnels = await query
            .OrderBy(input.Sorting)
            .PageBy(input)
            .ToListAsync();

            var saleFunnelListDtos = saleFunnels.MapTo<List<SaleFunnelListDto>>();
            foreach (var item in saleFunnelListDtos)
            {
                item.ClientTypeName = _clientTypeRepository.FirstOrDefault(item.ClientType)==null?"": _clientTypeRepository.FirstOrDefault(item.ClientType).ClientTypeName;
                item.ProductTypeName = _productTypeRepository.FirstOrDefault(item.ProductType).ProductTypeName;
                item.DistrictName = _districtRepository.FirstOrDefault(item.District).DistrictName;
            }
            return new PagedResultDto<SaleFunnelListDto>(
            saleFunnelCount,
            saleFunnelListDtos
            );
        }

        /// <summary>
        /// 通过Id获取销售漏斗信息进行编辑或修改 
        /// </summary>
        public async Task<GetSaleFunnelForEditOutput> GetSaleFunnelForEditAsync(NullableIdDto<System.Guid> input)
        {
            var output = new GetSaleFunnelForEditOutput();

            SaleFunnelEditDto saleFunnelEditDto;

            if (input.Id.HasValue)
            {
                var entity = await _saleFunnelRepository.GetAsync(input.Id.Value);
                saleFunnelEditDto = entity.MapTo<SaleFunnelEditDto>();
            }
            else
            {
                saleFunnelEditDto = new SaleFunnelEditDto();
            }
            //区域列表
            var allDistrict = await _districtRepository.GetAllListAsync();
            var parentDistrict = allDistrict.Where(s => s.ParentDistrictId < 1);
            var allDistrictList = new List<ComboboxItemDto>();
            //allDistrictList.Add(new ComboboxItemDto() { Value = "0", DisplayText = "请选择" });
            foreach (var item in parentDistrict)
            {

                allDistrictList.Add(new ComboboxItemDto() { Value = item.Id.ToString(), DisplayText = item.DistrictName });
                SetDistrictSelect(allDistrict, allDistrictList, item, "--");
            }
            //allDistrictList.ForEach(s => s.Value.Equals(saleFunnelEditDto.District.ToString()) ? s.IsSelected = true : s.IsSelected = false);
            foreach (var item in allDistrictList)
            {
                if (item.Value.Equals(saleFunnelEditDto.District))
                {
                    item.IsSelected = true;
                }
            }
            output.Districts = allDistrictList;
            //客户类型
            var allClientType = await _clientTypeRepository.GetAllListAsync();
            var allClientTypeList = new List<ComboboxItemDto>();
            //allDistrictList.Add(new ComboboxItemDto() { Value = "0", DisplayText = "请选择" });
            foreach (var item in allClientType)
            {
                allClientTypeList.Add(new ComboboxItemDto() { Value = item.Id.ToString(), DisplayText = item.ClientTypeName });
            }
            foreach (var item in allClientTypeList)
            {
                if (item.Value.Equals(saleFunnelEditDto.ClientType.ToString()))
                {
                    item.IsSelected = true;
                }
            }
            output.ClientTypes = allClientTypeList;
            //产品类型
            var allProductTypet = await _productTypeRepository.GetAllListAsync();
            var allProductTypeList = new List<ComboboxItemDto>();
            //allDistrictList.Add(new ComboboxItemDto() { Value = "0", DisplayText = "请选择" });
            foreach (var item in allProductTypet)
            {
                allProductTypeList.Add(new ComboboxItemDto() { Value = item.Id.ToString(), DisplayText = item.ProductTypeName });
            }
            foreach (var item in allProductTypeList)
            {
                if (item.Value.Equals(saleFunnelEditDto.ProductType.ToString()))
                {
                    item.IsSelected = true;
                }
            }
            output.ProductTypes = allProductTypeList;

            output.SaleFunnel = saleFunnelEditDto;
            return output;
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


        /// <summary>
        /// 通过指定id获取销售漏斗ListDto信息
        /// </summary>
        public async Task<SaleFunnelListDto> GetSaleFunnelByIdAsync(EntityDto<System.Guid> input)
        {
            var entity = await _saleFunnelRepository.GetAsync(input.Id);

            return entity.MapTo<SaleFunnelListDto>();
        }

        /// <summary>
        /// 新增或更改销售漏斗
        /// </summary>
        public async Task CreateOrUpdateSaleFunnelAsync(CreateOrUpdateSaleFunnelInput input)
        {
            if (input.SaleFunnelEditDto.Id.HasValue)
            {
                await UpdateSaleFunnelAsync(input.SaleFunnelEditDto);
            }
            else
            {
                await CreateSaleFunnelAsync(input.SaleFunnelEditDto);
            }
        }

        /// <summary>
        /// 新增销售漏斗
        /// </summary>
        [AbpAuthorize(SaleFunnelAppPermissions.SaleFunnel_CreateSaleFunnel)]
        public virtual async Task<SaleFunnelEditDto> CreateSaleFunnelAsync(SaleFunnelEditDto input)
        {
            //TODO:新增前的逻辑判断，是否允许新增

            var entity = input.MapTo<SaleFunnel>();
            // entity.Id = new Guid();
            entity.StatementTime = entity.StatementTime ==Convert.ToDateTime("0001 / 1 / 1 0:00:00") ? DateTime.Now : entity.StatementTime;
            entity.NextTime = entity.NextTime == Convert.ToDateTime("0001 / 1 / 1 0:00:00") ? DateTime.Now : entity.NextTime;
            entity.LastTime = entity.LastTime == Convert.ToDateTime("0001 / 1 / 1 0:00:00") ? DateTime.Now : entity.LastTime;
            entity.StageTime = entity.StageTime == Convert.ToDateTime("0001 / 1 / 1 0:00:00") ? DateTime.Now : entity.StageTime;
            entity.CreationTime = DateTime.Now;
            entity.CreateBy = GetCurrentUser().UserName;
            entity.UpdateBy = GetCurrentUser().UserName;

            entity.UpdateTime = DateTime.Now;
            entity.SumPrice =entity.Price * entity.Number;
            //entity.CreateBy=user
            entity = await _saleFunnelRepository.InsertAsync(entity);
            return entity.MapTo<SaleFunnelEditDto>();
        }

        /// <summary>
        /// 编辑销售漏斗
        /// </summary>
        [AbpAuthorize(SaleFunnelAppPermissions.SaleFunnel_EditSaleFunnel)]
        public virtual async Task UpdateSaleFunnelAsync(SaleFunnelEditDto input)
        {
            //TODO:更新前的逻辑判断，是否允许更新

            var entity = await _saleFunnelRepository.GetAsync(input.Id.Value);
            input.MapTo(entity);
            entity.UpdateTime = DateTime.Now;
            entity.UpdateBy = GetCurrentUser().UserName;

            await _saleFunnelRepository.UpdateAsync(entity);
        }

        /// <summary>
        /// 删除销售漏斗
        /// </summary>
        [AbpAuthorize(SaleFunnelAppPermissions.SaleFunnel_DeleteSaleFunnel)]
        public async Task DeleteSaleFunnelAsync(EntityDto<System.Guid> input)
        {
            //TODO:删除前的逻辑判断，是否允许删除
            await _saleFunnelRepository.DeleteAsync(input.Id);
        }

        /// <summary>
        /// 批量删除销售漏斗
        /// </summary>
        [AbpAuthorize(SaleFunnelAppPermissions.SaleFunnel_DeleteSaleFunnel)]
        public async Task BatchDeleteSaleFunnelAsync(List<System.Guid> input)
        {
            //TODO:批量删除前的逻辑判断，是否允许删除
            await _saleFunnelRepository.DeleteAsync(s => input.Contains(s.Id));
        }


        public List<SaleData> GetData(string input)
        {
            List<SaleData> saleList = new List<SaleData>();
            int keyvalue = input == "" ? DateTime.Now.Year : int.Parse(input);
            var query = _saleFunnelRepositoryAsNoTrack.ToList().Where(s => s.StatementTime.Year == keyvalue);
            for (int i = 1; i < 10; i += 2)
            {
                var partPrice = from r in query
                                where r.Stage == $"{i * 10}%"
                                group r by r.StatementTime.Month into g
                                select new { key = g.Key, value = g.Sum(s =>Convert.ToDecimal(s.SumPrice)) };
                var sumPrice =
                               from m in new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 }
                               join x in partPrice.ToList() on m equals x.key into all
                               from k in all.DefaultIfEmpty()
                               select k == null ? 0 : k.value;

                var serie = new SaleData
                {
                    name = $"{i * 10}%",
                    data = sumPrice.ToList()
                };
                saleList.Add(serie);

            }
            return saleList;

        }
        public SaleModelData GetAreaData(string input)
        {
            SaleModelData salemodeldata = new SaleModelData();
            List<SaleData> saleList = new List<SaleData>();
            int keyvalue = input == "" ? DateTime.Now.Year : int.Parse(input);
            var query = _saleFunnelRepositoryAsNoTrack.ToList().Where(s => s.StatementTime.Year == keyvalue);

            var lst = _districtRepository.GetAll().AsNoTracking().ToList();
            var strlst = new List<string>();
            var strnamelst = new List<string>();
            foreach (var item in lst)
            {
                strlst.Add(item.Id.ToString());
                strnamelst.Add(item.DistrictName);
            }
            var strname = strnamelst.ToArray();
            var str = strlst.ToArray();

            for (int i = 1; i < 4; i++)
            {
                var partPrice = from r in query
                                where r.StatementTime < DateTime.Now && r.StatementTime > DateTime.Now.AddDays(-30 * i)
                                group r by r.District.ToString() into g
                                select new { key = g.Key, value = g.Sum(s =>Convert.ToDecimal(s.SumPrice)) };
                var sumPrice =
                               from m in str /*new string[] { "南区区域", "北区区域", "西区区域", "东区区域", "大客户", "其他" }*/
                               join x in partPrice.ToList() on m equals x.key into all
                               from k in all.DefaultIfEmpty()
                               select k == null ? 0 : k.value;

                var serie = new SaleData
                {
                    name = $"{i * 30}天",
                    data = sumPrice.ToList()
                };
                saleList.Add(serie);

            }
            //本年未结
            var partfPrice = from r in query
                             where r.Stage != "100%"
                             group r by r.District.ToString() into g
                             select new { key = g.Key, value = g.Sum(s =>Convert.ToDecimal(s.SumPrice)) };
            var sumfPrice =
                           from m in str/*new string[] { "南区区域", "北区区域", "西区区域", "东区区域", "大客户", "其他" }*/
                           join x in partfPrice.ToList() on m equals x.key into all
                           from k in all.DefaultIfEmpty()
                           select k == null ? 0 : k.value;

            var serief = new SaleData
            {
                name = "本年未结",
                data = sumfPrice.ToList()
            };
            saleList.Add(serief);
            //本年已结
            var parttPrice = from r in query
                             where r.Stage == "100%"
                             group r by r.District.ToString() into g
                             select new { key = g.Key, value = g.Sum(s =>Convert.ToDecimal(s.SumPrice)) };
            var sumtPrice =
                           from m in str /*new string[] { "南区区域", "北区区域", "西区区域", "东区区域", "大客户", "其他" }*/
                           join x in parttPrice.ToList() on m equals x.key into all
                           from k in all.DefaultIfEmpty()
                           select k == null ? 0 : k.value;

            var seriet = new SaleData
            {
                name = "本年已结",
                data = sumtPrice.ToList()
            };
            saleList.Add(seriet);

            salemodeldata.xname = strname;
            salemodeldata.saledata = saleList;

            return salemodeldata;

        }

        public SaleModelData GetHospitalData(string input)
        {
            SaleModelData salemodeldata = new SaleModelData();
            List<SaleData> saleList = new List<SaleData>();
            int keyvalue = input == "" ? DateTime.Now.Year : int.Parse(input);
            var query = _saleFunnelRepositoryAsNoTrack.ToList().Where(s => s.StatementTime.Year == keyvalue);

            var lst = _clientTypeRepository.GetAll().AsNoTracking().ToList();
            var strlst = new List<string>();
            var strnamelst = new List<string>();
            foreach (var item in lst)
            {
                strlst.Add(item.Id.ToString());
                strnamelst.Add(item.ClientTypeName);
            }
            var strname = strnamelst.ToArray();
            var str = strlst.ToArray();

            for (int i = 1; i < 4; i++)
            {
                var partPrice = from r in query
                                where r.StatementTime < DateTime.Now && r.StatementTime > DateTime.Now.AddDays(-30 * i)
                                group r by r.ClientType.ToString() into g
                                select new { key = g.Key, value = g.Sum(s =>Convert.ToDecimal(s.SumPrice)) };
                var sumPrice =
                               from m in str/*new string[] { "三级甲等", "三级乙等", "二级甲等", "二级以下", "军队医院", "民营医院", "口腔诊所", "其他" }*/
                               join x in partPrice.ToList() on m equals x.key into all
                               from k in all.DefaultIfEmpty()
                               select k == null ? 0 : k.value;

                var serie = new SaleData
                {
                    name = $"{i * 30}天",
                    data = sumPrice.ToList()
                };
                saleList.Add(serie);

            }
            //本年未结
            var partfPrice = from r in query
                             where r.Stage != "100%"
                             group r by r.ClientType.ToString() into g
                             select new { key = g.Key, value = g.Sum(s =>Convert.ToDecimal(s.SumPrice)) };
            var sumfPrice =
                               from m in str /*new string[] { "三级甲等", "三级乙等", "二级甲等", "二级以下", "军队医院", "民营医院", "口腔诊所", "其他" }*/
                               join x in partfPrice.ToList() on m equals x.key into all
                               from k in all.DefaultIfEmpty()
                               select k == null ? 0 : k.value;

            var serief = new SaleData
            {
                name = "本年未结",
                data = sumfPrice.ToList()
            };
            saleList.Add(serief);
            //本年已结
            var parttPrice = from r in query
                             where r.Stage == "100%"
                             group r by r.ClientType.ToString() into g
                             select new { key = g.Key, value = g.Sum(s =>Convert.ToDecimal(s.SumPrice)) };
            var sumtPrice =
                               from m in str /*new string[] { "三级甲等", "三级乙等", "二级甲等", "二级以下", "军队医院", "民营医院", "口腔诊所", "其他" }*/
                               join x in parttPrice.ToList() on m equals x.key into all
                               from k in all.DefaultIfEmpty()
                               select k == null ? 0 : k.value;

            var seriet = new SaleData
            {
                name = "本年已结",
                data = sumtPrice.ToList()
            };
            saleList.Add(seriet);
            salemodeldata.xname = strname;
            salemodeldata.saledata = saleList;

            return salemodeldata;

        }

        public List<SalePieData> GetPieData(string input)
        {
            SalePieData spd = new SalePieData();
            int keyvalue = input == "" ? DateTime.Now.Year : int.Parse(input);
            var query = _saleFunnelRepositoryAsNoTrack.ToList().Where(s => s.StatementTime.Year == keyvalue);
            var lst = _clientTypeRepository.GetAll().AsNoTracking().ToList();
            var strlst = new List<string>();
            foreach (var item in lst)
            {
                strlst.Add(item.Id.ToString());
            }
            var str = strlst.ToArray();

            var sum = query.Sum(s => Convert.ToDecimal(s.SumPrice));
            var parttPrice = from r in query
                             group r by r.ClientType.ToString() into g
                             select new { key = g.Key, value = (g.Sum(s => Convert.ToDecimal(s.SumPrice)) * 100) / sum };
            var sumtPrice =
                               from m in str /*new string[] { "三级甲等", "三级乙等", "二级甲等", "二级以下", "军队医院", "民营医院", "口腔诊所", "其他" }*/
                               join x in parttPrice.ToList() on m equals x.key into all
                               from k in all.DefaultIfEmpty()
                               select new SalePieData { key = m, value = (k == null ? 0 : k.value) };

            //var seriet = new SalePieData
            //{
            //    name = "本年已结",
            //    data = sumtPrice.ToList()
            //};
            var lstsum = sumtPrice.ToList();
            foreach (var item in lstsum)
            {
                item.key = _clientTypeRepository.FirstOrDefault(Convert.ToInt32(item.key)).ClientTypeName;
            }

            return lstsum;
        }

        public List<SalePieData> GetAreaPieData(string input)
        {
            SalePieData spd = new SalePieData();
            int keyvalue = input == "" ? DateTime.Now.Year : int.Parse(input);
            var query = _saleFunnelRepositoryAsNoTrack.ToList().Where(s => s.StatementTime.Year == keyvalue);
            var lst = _districtRepository.GetAll().AsNoTracking().ToList();
            var strlst = new List<string>();
            foreach (var item in lst)
            {
                strlst.Add(item.Id.ToString());
            }
            var str = strlst.ToArray();

            var sum = query.Sum(s => Convert.ToDecimal(s.SumPrice));
            var parttPrice = from r in query
                             group r by r.District.ToString() into g
                             select new { key = g.Key, value = (g.Sum(s => Convert.ToDecimal(s.SumPrice)) * 100) / sum };
            var sumtPrice =
                               from m in str /*new string[] { "南区区域", "北区区域", "西区区域", "东区区域", "大客户", "其他" }*/
                               join x in parttPrice.ToList() on m equals x.key into all
                               from k in all.DefaultIfEmpty()
                               select new SalePieData { key = m, value = (k == null ? 0 : k.value) };

            //var seriet = new SalePieData
            //{
            //    name = "本年已结",
            //    data = sumtPrice.ToList()
            //};
            var lstsum = sumtPrice.ToList();
            foreach (var item in lstsum)
            {
                item.key = _districtRepository.GetAll().Where(s => s.Id.ToString().Equals(item.key)).FirstOrDefault().DistrictName;
            }

            return lstsum;

        }

        public List<SalePieData> GetProductTypePieData(string input)
        {
            SalePieData spd = new SalePieData();
            int keyvalue = input == "" ? DateTime.Now.Year : int.Parse(input);
            var query = _saleFunnelRepositoryAsNoTrack.ToList().Where(s => s.StatementTime.Year == keyvalue);

            var lst = _productTypeRepository.GetAll().AsNoTracking().ToList();
            var strlst = new List<string>();
            foreach (var item in lst)
            {
                strlst.Add(item.Id.ToString());
            }
            var str = strlst.ToArray();

            var sum = query.Sum(s => Convert.ToDecimal(s.SumPrice));
            var parttPrice = from r in query
                             group r by r.ProductType.ToString() into g
                             select new { key = g.Key, value = (g.Sum(s => Convert.ToDecimal(s.SumPrice)) * 100) / sum };
            var sumtPrice =
                               from m in str /*new string[] { "影像", "技工", "牙椅", "手机", "教学", "其他设备" }*/
                               join x in parttPrice.ToList() on m equals x.key into all
                               from k in all.DefaultIfEmpty()
                               select new SalePieData { key = m, value = (k == null ? 0 : k.value/*.GetValueOrDefault()*/) };

            //var seriet = new SalePieData
            //{
            //    name = "本年已结",
            //    data = sumtPrice.ToList()
            //};
            var lstsum = sumtPrice.ToList();
            foreach (var item in lstsum)
            {
                item.key = _productTypeRepository.GetAll().Where(s => s.Id.ToString().Equals(item.key)).FirstOrDefault().ProductTypeName;
            }
            return lstsum;

        }

        #endregion
        #region 销售漏斗的Excel导出功能


        public async Task<FileDto> GetSaleFunnelToExcel()
        {
            var entities = await _saleFunnelRepository.GetAll().ToListAsync();

            var dtos = entities.MapTo<List<SaleFunnelListDto>>();

            var fileDto = _saleFunnelListExcelExporter.ExportSaleFunnelToFile(dtos);



            return fileDto;
        }


        #endregion
    }
    public class SaleData
    {
        public string name { get; set; }
        public List<decimal> data { get; set; }
    }
    public class SaleModelData
    {
        public string[] xname { get; set; }
        public List<SaleData> saledata { get; set; }
    }

    public class SalePieData
    {
        [Newtonsoft.Json.JsonProperty("name")]
        public string key { get; set; }
        [Newtonsoft.Json.JsonProperty("y")]
        public decimal value { get; set; }
    }

}
