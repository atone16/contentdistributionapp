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
    public class ContentDistributionManager : IContentDistributionManager
    {
        private readonly IContentDistributionAccess contentDistributionAccess;
        private readonly IMapper mapper;

        public ContentDistributionManager(
            IContentDistributionAccess contentDistributionAccess,
            IMapper mapper
            ) 
        { 
            this.contentDistributionAccess = contentDistributionAccess;
            this.mapper = mapper;
        }

        public async Task<bool> ArchiveContentDistribution(Guid id)
        {
            try
            {
                var contentDistribution = await this.contentDistributionAccess.GetByIdAsync(id);
                contentDistribution.IsArchived = true;
                await this.contentDistributionAccess.UpdateAsync(contentDistribution);
                return true;

            }
            catch (Exception ex)
            {
                throw new Exception($"Error Archiving ContentDistribution. {id}. {ex.Message}");
            }
        }

        public async Task<ContentDistributionDto> CreateContentDistribution(ContentDistributionInput input)
        {
            var inputContentDistribution = this.mapper.Map<ContentDistribution>(input);
            var createdContentDistribution = await this.contentDistributionAccess.CreateAsync(inputContentDistribution);
            return this.mapper.Map<ContentDistributionDto>(createdContentDistribution);
        }

        public async Task<ContentDistributionDto> GetById(Guid id)
        {
            var contentDistribution = await this.contentDistributionAccess.GetByIdAsync(id);
            return this.mapper.Map<ContentDistributionDto>(contentDistribution);
        }

        public async Task<ContentDistributionDto> UpdateContentDistribution(Guid id, ContentDistributionInput input)
        {
            try
            {
                var contentDistribution = await this.contentDistributionAccess.GetByIdAsync(id);

                if (contentDistribution == null)
                {
                    throw new Exception("Cannot Update A NonExistent ContentDistribution.");
                }

                if (contentDistribution.IsArchived)
                {
                    throw new Exception("Cannot Update An Archived ContentDistribution.");
                }

                contentDistribution.DistributionChannels = input.DistributionChannels;
                contentDistribution.DistributionMethods = input.DistributionMethods;
                contentDistribution.DistributionDate = input.DistributionDate;

                var updatedContentDistribution = await this.contentDistributionAccess.UpdateAsync(contentDistribution);
                return this.mapper.Map<ContentDistributionDto>(updatedContentDistribution);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error Updating ContentDistribution. {id}. {ex.Message}");
            }
        }

    }
}
