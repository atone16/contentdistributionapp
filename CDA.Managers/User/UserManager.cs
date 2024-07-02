using AutoMapper;
using CDA.Data;
using CDA.IAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDA.Managers
{
    public class UserManager : IUserManager
    {
        private readonly IUserAccess userAccess;
        private readonly IMapper mapper;

        public UserManager(IUserAccess userAccess, IMapper mapper) 
        {
            this.mapper = mapper;
            this.userAccess = userAccess;
        }

        public async Task<bool> ArchiveUser(Guid id)
        {
            try
            {
                var user = await this.userAccess.GetByIdAsync(id);
                user.IsArchived = true;
                await this.userAccess.UpdateAsync(user);
                return true;

            }
            catch (Exception ex)
            {
                throw new Exception($"Error archiving user. {id}. {ex.Message}");
            }
        }

        public async Task<UserDto> CreateUser(UserInput input)
        {
            var inputUser = this.mapper.Map<User>(input);
            var createdUser = await this.userAccess.CreateAsync(inputUser);
            return this.mapper.Map<UserDto>(createdUser);
        }

        public async Task<UserDto> GetById(Guid id)
        {
            var user = await this.userAccess.GetByIdAsync(id);
            return this.mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> UpdateUser(Guid id, UserInput input)
        {
            try
            {
                var user = await this.userAccess.GetByIdAsync(id);

                if (user == null)
                {
                    throw new Exception("Cannot update non existent user.");
                }

                if (user.IsArchived)
                {
                    throw new Exception("Cannot update an archived user.");
                }

                user.FirstName = input.FirstName ?? user.FirstName;
                user.LastName = input.LastName ?? user.LastName;
                user.Email = input.Email ?? user.Email;

                var updatedUser = await this.userAccess.UpdateAsync(user);
                return this.mapper.Map<UserDto>(updatedUser);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error Updating User. {id}. {ex.Message}");
            }
        }
    }
}
