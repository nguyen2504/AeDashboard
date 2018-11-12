using System.Threading.Tasks;
using Abp.Application.Services;
using AeDashboard.Authorization.Accounts.Dto;

namespace AeDashboard.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
