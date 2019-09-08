using System.Threading.Tasks;
using Abp.Domain.Policies;

namespace LY.PF.Authorization.Users
{
    public interface IUserPolicy : IPolicy
    {
        Task CheckMaxUserCountAsync(int tenantId);
    }
}
