using System.Collections.Generic;
using Abp.Application.Services.Dto;
using LY.PF.Editions.Dto;

namespace LY.PF.MultiTenancy.Dto
{
    public class GetTenantFeaturesForEditOutput
    {
        public List<NameValueDto> FeatureValues { get; set; }

        public List<FlatFeatureDto> Features { get; set; }
    }
}