using System.Threading.Tasks;
using LY.PF.Sessions.Dto;

namespace LY.PF.Web.Session
{
    public interface IPerRequestSessionCache
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformationsAsync();
    }
}
