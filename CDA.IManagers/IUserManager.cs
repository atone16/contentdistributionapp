﻿using CDA.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDA.IManagers
{
    public interface IUserManager
    {
        Task<UserDto> CreateUser(UserInput input);
        Task<UserDto> UpdateUser(Guid id, UserInput input);
        Task<bool> ArchiveUser(Guid id, Guid tenantId);
        Task<UserDto> GetById(Guid id, Guid tenantId);
        Task<List<UserDto>> GetByTenantId(Guid tenantId);
    }
}
