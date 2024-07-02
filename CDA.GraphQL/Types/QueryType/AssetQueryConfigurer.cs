using CDA.Data;
using CDA.GraphQL.Queries;
using CDA.IManagers;

namespace CDA.GraphQL.Types.QueryType
{
    public static class AssetQueryConfigurer
    {
        public static void Configure(IObjectTypeDescriptor<Query> descriptor)
        {
            descriptor.Field("asset")
                .Name("asset")
                .Description("the asset")
                .Argument(
                    "id",
                    a => a
                        .Type<NonNullType<StringType>>()
                        .Description("The Id of the asset to find."))
                .Type<AssetType>()
                .Resolve(async context =>
                {
                    var id = context.ArgumentValue<string>("id");
                    if (Guid.TryParse(id, out Guid guidId))
                    {
                        var assetManager = context.Service<IAssetManager>();
                        return await assetManager.GetById(guidId);
                    }
                    throw new Exception("Invalid input parameters");
                });
        }
    }
}
