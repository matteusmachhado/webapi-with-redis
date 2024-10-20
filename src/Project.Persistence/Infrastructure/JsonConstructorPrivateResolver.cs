using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Reflection;

namespace Project.Persistence.Infrastructure
{
    public class JsonConstructorPrivateResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(
            MemberInfo member,
            MemberSerialization memberSerialization)
        {
            JsonProperty prop = base.CreateProperty(
                member,
                memberSerialization);

            if (!prop.Writable)
            {
                var property = member as PropertyInfo;

                bool hasPrivateSetter = property?.GetSetMethod(true) != null;

                prop.Writable = hasPrivateSetter;
            }

            return prop;
        }
    }
}
