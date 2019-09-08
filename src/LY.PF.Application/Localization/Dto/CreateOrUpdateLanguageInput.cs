using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace LY.PF.Localization.Dto
{
    public class CreateOrUpdateLanguageInput
    {
        [Required]
        public ApplicationLanguageEditDto Language { get; set; }
    }
}