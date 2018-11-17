using System;
using System.Collections.Generic;
using System.Text;
using Abp.Domain.Services;
using AeDashboard.Authorization.Users;

namespace AeDashboard.GetUser
{
    class GetUserService:DomainService, IGetUserService
    {
        private readonly UserManager _userManager;

        public GetUserService(UserManager userManager)
        {
            _userManager = userManager;
        }
        public long GetIdUser()
        {
            var id = _userManager.AbpSession.UserId;
            if (id>0)
            {
                return (long)id;
            }
            return 0;
        }
    }
}
