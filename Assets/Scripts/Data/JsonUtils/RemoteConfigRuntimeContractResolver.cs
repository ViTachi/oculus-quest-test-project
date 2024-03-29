using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections;
using System.Reflection;
internal class RemoteConfigRuntimeContractResolver : RemoteConfigContractResolver
{
    protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
    {
        JsonProperty property = base.CreateProperty(member, memberSerialization);
        if (property.Ignored) return property;

        // it's need to replace collections, but not other class-based objects
        if (property.PropertyType.IsArray || typeof(IEnumerable).IsAssignableFrom(property.PropertyType))
        {
            property.ObjectCreationHandling = ObjectCreationHandling.Replace;
        }

        return property;

        //JsonProperty property = base.CreateProperty(member, memberSerialization);
        //if (property.Ignored) return property;

        //var attribute = GetPropertyAttribute<RemoteConfigMergeStrategyAttribute>(property);
        //if (attribute == null)
        //{
        //    ApplyMergeStrategy(property, GetDefaultMergeStrategy(property));
        //}
        //else if (attribute.IsCustomMergeStrategy)
        //{
        //    ApplyMergeStrategy(property, attribute.CustomMergeStrategy);
        //}
        //else
        //{
        //    MergeStrategy mergeStrategy = attribute.MergeStrategy;
        //    if (mergeStrategy == MergeStrategy.None)
        //        mergeStrategy = GetDefaultMergeStrategy(property);

        //    ApplyMergeStrategy(property, mergeStrategy);
        //}

        //return property;
    }

    private MergeStrategy GetDefaultMergeStrategy(JsonProperty property)
        => IsObject(property) ? MergeStrategy.Join : MergeStrategy.Replace; // by default use Join for objects and Replace for arrays

    private void ApplyMergeStrategy(JsonProperty property, MergeStrategy mergeStrategy)
    {
        switch (mergeStrategy)
        {
            case MergeStrategy.Join:
                property.ObjectCreationHandling = ObjectCreationHandling.Reuse;
                property.Converter = new JoinMergeJsonConverter();
                break;

            case MergeStrategy.Replace:
                property.ObjectCreationHandling = ObjectCreationHandling.Replace;
                break;

            case MergeStrategy.Append:
                property.ObjectCreationHandling = ObjectCreationHandling.Reuse;
                break;

            default:
                throw new NotSupportedException(mergeStrategy.ToString());
        }
    }

    private void ApplyMergeStrategy(JsonProperty property, Type mergeStrategyType)
    {
        var mergeStrategy = Activator.CreateInstance(mergeStrategyType) as ICustomMergeStrategy;
        property.Converter = new CustomMergeJsonConverter(mergeStrategy);
    }
}

public interface ICustomMergeStrategy
{
    object Merge(object local, object remote);
}

public enum MergeStrategy
{
    None = 0,

    Join,
    Replace,
    Append
}

