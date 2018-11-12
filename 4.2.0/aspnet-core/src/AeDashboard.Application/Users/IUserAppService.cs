using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using AeDashboard.Roles.Dto;
using AeDashboard.Users.Dto;

namespace AeDashboard.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedResultRequestDto, CreateUserDto, UserDto>
    {
        Task<ListResultDto<RoleDto>> GetRoles();

        Task ChangeLanguage(ChangeUserLanguageDto input);
    }
}
