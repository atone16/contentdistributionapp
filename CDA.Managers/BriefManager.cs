using AutoMapper;
using CDA.Data;
using CDA.IAccess;
using CDA.IManagers;

namespace CDA.Managers
{
    public class BriefManager : IBriefManager
    {
        private readonly IBriefAccess briefAccess;
        private readonly IBriefCommentAccess briefCommentAccess;
        private readonly IMapper mapper;

        public BriefManager(
            IBriefAccess briefAccess,
            IBriefCommentAccess briefCommentAccess,
            IMapper mapper
        ) 
        {
            this.briefAccess = briefAccess;
            this.briefCommentAccess = briefCommentAccess;
            this.mapper = mapper;
        }

        public async Task<bool> ArchiveBrief(Guid id, Guid tenantId, Guid userId)
        {
            try
            {
                var brief = await this.briefAccess.GetByIdAsync(id);

                if (brief.TenantId != tenantId)
                {
                    throw new Exception("Cant archive a user not within the same tenant");
                }

                brief.IsArchived = true;
                brief.LastUpdatedBy = userId;
                await this.briefAccess.UpdateAsync(brief);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error archiving brief. {id}. {ex.Message}");
            }
        }

        public async Task<BriefDto> CreateBrief(BriefInput input)
        {
            var inputBrief = this.mapper.Map<Brief>(input);
            inputBrief.CreatedBy = input.UserId;
            var createdBrief = await this.briefAccess.CreateAsync(inputBrief);
            return this.mapper.Map<BriefDto>(createdBrief);
        }

        public async Task<BriefDto> GetById(Guid id)
        {
            var brief = await this.briefAccess.GetByIdAsync(id);
            return this.mapper.Map<BriefDto>(brief);
        }

        public async Task<BriefDto> GetByBriefId(string briefId)
        {
            var brief = await this.briefAccess.GetByBriefId(briefId);
            return this.mapper.Map<BriefDto>(brief);
        }

        public async Task<BriefDto> UpdateBrief(Guid id, BriefInput input)
        {
            try
            {
                var brief = await this.briefAccess.GetByIdAsync(id);

                if (brief == null)
                {
                    throw new Exception("Cannot update non existent brief.");
                }

                if (brief.IsArchived)
                {
                    throw new Exception("Cannot update an archived brief.");
                }

                brief.Description = input.Description ?? brief.Description;
                brief.Name = input.Name ?? brief.Name;
                brief.Status = input.Status != brief.Status ? input.Status : brief.Status;
                brief.LastUpdatedBy = input.UserId;

                var updatedBrief = await this.briefAccess.UpdateAsync(brief);
                return this.mapper.Map<BriefDto>(updatedBrief);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error Updating Brief. {id}. {ex.Message}");
            }
        }

        public async Task<List<BriefDto>> GetByTenantId(Guid tenantId)
        {
            var briefs = await this.briefAccess.GetByTenantId(tenantId);
            return this.mapper.Map<List<BriefDto>>(briefs);
        }

        public async Task<BriefCommentDto> AddBriefComment(BriefCommentInput input)
        {
            var inputBriefComment = this.mapper.Map<BriefComment>(input);
            inputBriefComment.CreatedBy = input.UserId;
            var createdBriefComment = await this.briefCommentAccess.CreateAsync(inputBriefComment);
            return this.mapper.Map<BriefCommentDto>(createdBriefComment);
        }

        public async Task<bool> RemoveBriefComment(Guid id, Guid tenantId, Guid userId)
        {
            try
            {
                var briefComment = await this.briefCommentAccess.GetByIdAsync(id);
                if (briefComment.TenantId != tenantId)
                {
                    throw new Exception("Cannot Delete BriefComment if not same tenant");
                }

                if (briefComment.CreatedBy != userId)
                {
                    throw new Exception("Cannot update a comment that is not created by the user.");
                }

                return await this.briefCommentAccess.RemoveAsync(id, briefComment.TenantId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting record. {ex.Message}");
            }
        }

        public async Task<BriefCommentDto> UpdateBriefComment(Guid id, BriefCommentInput input)
        {
            try
            {
                var briefComment = await this.briefCommentAccess.GetByIdAsync(id);

                if (briefComment == null)
                {
                    throw new Exception("Cannot update non existent brief comment.");
                }

                if (briefComment.IsArchived)
                {
                    throw new Exception("Cannot update an archived brief comment.");
                }

                if (briefComment.CreatedBy != input.UserId)
                {
                    throw new Exception("Cannot update a comment that is not created by the user.");
                }

                briefComment.Comment = input.Comment;
                briefComment.LastUpdatedBy = input.UserId;

                var updatedBriefComment = await this.briefCommentAccess.UpdateAsync(briefComment);
                return this.mapper.Map<BriefCommentDto>(updatedBriefComment);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error Updating Brief Comment. {id}. {ex.Message}");
            }
        }

        public async Task<List<BriefCommentDto>> GetByBriefId(Guid briefId, Guid tenantId)
        {
            var briefComments = await this.briefCommentAccess.GetByBriefId(briefId, tenantId);
            return this.mapper.Map<List<BriefCommentDto>>(briefComments);
        }
    }
}
