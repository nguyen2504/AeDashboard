using System.Threading.Tasks;
using Abp.Application.Services;
using AeDashboard.Sessions.Dto;

namespace AeDashboard.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
