using CDA.Access;
using CDA.Core;
using CDA.Data;
using CDA.Data.Enum;
using CDA.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CDA.AccessTests
{
    [TestClass]
    public class AssetAccessTests
    {
        private void Setup(out List<Asset> asset)
        {
            asset = new List<Asset>()
            {
                new Asset
                {

                },
                new Asset
                {

                }
            };
        }

        private IDateTimeProvider MockDateTimeProvider()
        {
            var mockDateTimeProvider = new Mock<IDateTimeProvider>();
            mockDateTimeProvider.Setup(x => x.UtcNow).Returns(DateTime.Parse("2024-07-04"));
            return mockDateTimeProvider.Object;
        }

        private IGuidGenerator MockGuidGenerator()
        {
            var mockGuidGenerator = new Mock<IGuidGenerator>();
            mockGuidGenerator.Setup(x => x.GenerateNewGuid()).Returns(Guid.Parse("0b91546e-aa05-4537-af71-ce415cca1814"));
            return mockGuidGenerator.Object;
        }

        private ICache MockCache()
        {
            var mockCache = new Mock<ICache>();
            return mockCache.Object;
        }

        [TestMethod]
        public async void AssetAccessCreate()
        {
            List<Asset> assets;
            Setup(out assets);

            var assetAccess = new AssetAccess(MockCache(), MockGuidGenerator(), MockDateTimeProvider() );

            var result = await assetAccess.CreateAsync(new Asset
            {
                AssetStatus = Data.Enum.Status.Approved
            });

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Id != Guid.Empty);
            Assert.IsTrue(result.TenantId != Guid.Empty);
            Assert.IsTrue(result.AssetStatus == Status.Approved);
        }

        [TestMethod]
        public async void AssetAccessUpdate()
        {
            List<Asset> assets;
            Setup(out assets);

            var assetAccess = new AssetAccess(MockCache(), MockGuidGenerator(), MockDateTimeProvider());

            var result = await assetAccess.UpdateAsync(new Asset
            {
                AssetStatus = Data.Enum.Status.Approved
            });

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Id != Guid.Empty);
            Assert.IsTrue(result.TenantId != Guid.Empty);
            Assert.IsTrue(result.AssetStatus == Status.Approved);
        }

        [TestMethod]
        public async void AssetAccessGetById()
        {
            List<Asset> assets;
            Setup(out assets);

            var assetAccess = new AssetAccess(MockCache(), MockGuidGenerator(), MockDateTimeProvider());

            var result = await assetAccess.CreateAsync(new Asset
            {
                AssetStatus = Data.Enum.Status.Approved
            });

            var getResult = await assetAccess.GetByIdAsync(result.Id);

            Assert.IsNotNull(getResult);
            Assert.IsTrue(getResult.Id == result.Id);
            Assert.IsTrue(getResult.TenantId == result.TenantId);
            Assert.IsTrue(getResult.AssetStatus == result.AssetStatus);
        }
    }
}
