using System.Collections.Generic;
using Abp.Application.Services.Dto;
using LY.PF.Editions.Dto;

namespace LY.PF.Web.Areas.Mpa.Models.Common
{
    public interface IFeatureEditViewModel
    {
        List<NameValueDto> FeatureValues { get; set; }

        List<FlatFeatureDto> Features { get; set; }
    }
}