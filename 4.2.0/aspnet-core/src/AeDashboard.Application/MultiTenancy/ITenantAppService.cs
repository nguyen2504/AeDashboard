using Abp.Application.Services;
using Abp.Application.Services.Dto;
using AeDashboard.MultiTenancy.Dto;

namespace AeDashboard.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}
