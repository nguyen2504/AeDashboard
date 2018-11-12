using System.Collections.Generic;
using AeDashboard.Roles.Dto;
using AeDashboard.Users.Dto;

namespace AeDashboard.Web.Models.Users
{
    public class UserListViewModel
    {
        public IReadOnlyList<UserDto> Users { get; set; }

        public IReadOnlyList<RoleDto> Roles { get; set; }
    }
}
