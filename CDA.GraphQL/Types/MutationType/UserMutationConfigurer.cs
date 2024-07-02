using CDA.Data;
using CDA.GraphQL.Mutations;
using CDA.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDA.GraphQL.Types.MutationType
{
    public static class UserMutationConfigurer
    {
        public static void Configure(IObjectTypeDescriptor<Mutation> descriptor)
        {
            descriptor.Field("createUser")
                .Description("Creates a new user.")
                .Type<UserType>()
                .Argument(
                    "userInput",
                    a => a
                        .Type<NonNullType<UserInputType>>()
                        .Description("The user information to create."))
                .Resolve(
                    async context =>
                    {
                        var input = context.ArgumentValue<UserInput>("userInput");
                        var userManager = context.Service<IUserManager>();
                        return await userManager.CreateUser(input);
                    }
                );

            descriptor.Field("updateUser")
                .Description("Updates an existing user.")
                .Type<UserType>()
                .Argument(
                    "id",
                    a => a
                        .Type<NonNullType<StringType>>()
                        .Description("The id of the user."))
                .Argument(
                    "userInput",
                    a => a
                        .Type<NonNullType<UserInputType>>()
                        .Description("The user information to update."))
                .Resolve(
                    async context =>
                    {
                        var id = context.ArgumentValue<string>("id");

                        if (Guid.TryParse(id, out Guid guidId))
                        {
                            var input = context.ArgumentValue<UserInput>("userInput");
                            var userManager = context.Service<IUserManager>();
                            return await userManager.UpdateUser(guidId, input);
                        }

                        throw new Exception("Invalid input parameters");
                    }
                );

            descriptor.Field("archiveUser")
                .Description("Archives a user.")
                .Type<BooleanType>()
                .Argument(
                    "id",
                    a => a
                        .Type<NonNullType<StringType>>()
                        .Description("The id of the user to archive."))
                .Resolve(
                    async context =>
                    {
                        var id = context.ArgumentValue<string>("id");

                        if (Guid.TryParse(id, out Guid guidId))
                        {
                            var userManager = context.Service<IUserManager>();
                            return await userManager.ArchiveUser(guidId);
                        }

                        throw new Exception("Invalid input parameters");
                    }
                );
        }
    }
}
