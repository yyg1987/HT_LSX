using System.Collections.Generic;
using LY.PF.ProductTypes.Dtos;
using LY.PF.Dto;

namespace LY.PF.ProductTypes
{
	/// <summary>
    /// 订单的数据导出功能 
    /// </summary>
    public interface IProductTypeListExcelExporter
    {
        
//## 可以将下面的这个实体类，作为filedto来进行接收 


    //public class FileDto
    //{
    //    [Required]
    //    public string FileName { get; set; }

    //    [Required]
    //    public string FileType { get; set; }

    //    [Required]
    //    public string FileToken { get; set; }

    //    public FileDto()
    //    {
            
    //    }

    //    public FileDto(string fileName, string fileType)
    //    {
    //        FileName = fileName;
    //        FileType = fileType;
    //        FileToken = Guid.NewGuid().ToString("N");
    //    }
    //}

        /// <summary>
        /// 导出订单到EXCEL文件
        /// <param name="productTypeListDtos">导出信息的DTO</param>
        /// </summary>
        FileDto ExportProductTypeToFile(List<ProductTypeListDto> productTypeListDtos);



    }
}
