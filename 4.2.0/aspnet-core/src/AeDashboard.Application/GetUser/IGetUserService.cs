using System;
using System.Collections.Generic;
using System.Text;
using Abp.Domain.Services;

namespace AeDashboard.GetUser
{
   public interface IGetUserService:IDomainService
   {
       long GetIdUser();
   }
}
