using System.Collections.Generic;
using AeDashboard.Roles.Dto;

namespace AeDashboard.Web.Models.Common
{
    public interface IPermissionsEditViewModel
    {
        List<FlatPermissionDto> Permissions { get; set; }
    }
}