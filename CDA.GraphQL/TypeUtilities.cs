using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CDA.GraphQL
{
    public static class TypeUtilities
    {
        public static Type[] GetGraphTypes()
        {
            Assembly asm = Assembly.GetExecutingAssembly();

            return asm.DefinedTypes.Where(
                    typeInfo => IsConcreteGraphType(typeInfo))
                .Select(typeInfo => typeInfo.AsType())
                .ToArray();
        }

        private static bool IsConcreteGraphType(TypeInfo typeInfo)
        {
            return !typeInfo.IsAbstract
                   && typeInfo.IsSubclassOf(typeof(ObjectType))
                   && !typeInfo.IsInstanceOfType(typeof(QueryType))
                   && !typeInfo.IsInstanceOfType(typeof(MutationType));
        }

        public static List<TypeInfo> GetQueryConfigurerTypes()
        {
            return GetConfigurerTypes("Query");
        }

        public static List<TypeInfo> GetMutationConfigurerTypes()
        {
            return GetConfigurerTypes("Mutation");
        }

        public static List<TypeInfo> GetSubscriptionConfigurerTypes()
        {
            return GetConfigurerTypes("Subscription");
        }

        private static List<TypeInfo> GetConfigurerTypes(string namePrefix)
        {
            var asm = Assembly.GetExecutingAssembly();

            return asm.DefinedTypes
                .Where(
                    typeInfo => IsQueryConfigurerType(typeInfo, namePrefix))
                .ToList();
        }

        private static bool IsQueryConfigurerType(TypeInfo typeInfo, string namePrefix)
        {
            return typeInfo.IsClass
                   && (typeInfo.Name.EndsWith($"{namePrefix}Configurer"))
                   && typeInfo.DeclaredMethods.Any(
                       methodInfo => methodInfo.IsStatic
                                     && methodInfo.Name == "Configure");
        }
    }
}
