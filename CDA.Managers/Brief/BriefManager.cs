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
    public class BriefManager : IBriefManager
    {
        private readonly IBriefAccess briefAccess;
        private readonly IMapper mapper;

        public BriefManager(
            IBriefAccess briefAccess,
            IMapper mapper
        ) 
        {
            this.briefAccess = briefAccess;
            this.mapper = mapper;
        }

        public async Task<bool> ArchiveBrief(Guid id)
        {
            try
            {
                var brief = await this.briefAccess.GetByIdAsync(id);
                brief.IsArchived = true;
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
            var createdBrief = await this.briefAccess.CreateAsync(inputBrief);
            return this.mapper.Map<BriefDto>(createdBrief);
        }

        public async Task<BriefDto> GetById(Guid id)
        {
            var brief = await this.briefAccess.GetByIdAsync(id);
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

                var updatedBrief = await this.briefAccess.UpdateAsync(brief);
                return this.mapper.Map<BriefDto>(updatedBrief);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error Updating Brief. {id}. {ex.Message}");
            }
        }
    }
}
