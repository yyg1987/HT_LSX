using System.Collections.Generic;
using LY.PF.Authorization.Users.Dto;
using LY.PF.Dto;

namespace LY.PF.Authorization.Users.Exporting
{
    public interface IUserListExcelExporter
    {
        FileDto ExportToFile(List<UserListDto> userListDtos);
    }
}