using AutoMapper;
using CDA.Data;
using CDA.IAccess;
using CDA.IManagers;

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

        public async Task<bool> ArchiveUser(Guid id, Guid tenantId)
        {
            try
            {
                var user = await this.userAccess.GetByIdAsync(id);

                if (user.TenantId != tenantId)
                {
                    throw new Exception("Cant archive a user not within the same tenant");
                }

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

        public async Task<UserDto> GetById(Guid id, Guid tenantId)
        {
            var user = await this.userAccess.GetByIdAsync(id);

            if (user.TenantId != tenantId)
            {
                throw new Exception("Cant return a user of not the same tenant");
            }

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

                user.FirstName = UpdateValue.GetUpdatedValue(input.FirstName, user.FirstName);
                user.LastName = UpdateValue.GetUpdatedValue(input.LastName, user.LastName);
                user.Email = UpdateValue.GetUpdatedValue(input.Email, user.Email);

                user.TenantId = input.TenantId;
                user.LastUpdatedBy = input.UserId;

                var updatedUser = await this.userAccess.UpdateAsync(user);
                return this.mapper.Map<UserDto>(updatedUser);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error Updating User. {id}. {ex.Message}");
            }
        }

        public async Task<List<UserDto>> GetByTenantId(Guid tenantId)
        {
            var users = await this.userAccess.GetByTenantId(tenantId);
            return this.mapper.Map<List<UserDto>>(users);
        }
    }
}
