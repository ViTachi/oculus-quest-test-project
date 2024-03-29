using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections;
using System.Linq;
using System.Reflection;

internal abstract class RemoteConfigContractResolver : DefaultContractResolver
{
    //protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
    //{
    //    JsonProperty property = base.CreateProperty(member, memberSerialization);

    //    var attribute = GetPropertyAttribute<JsonPropertyAttribute>(property);
    //    if (attribute == null)
    //    {
    //        property.Ignored = true;
    //        return property;
    //    }

    //    return property;
    //}

    protected bool HasJsonPropertyAttribute(JsonProperty property)
        => GetPropertyAttribute<JsonPropertyAttribute>(property) != null;

    protected bool IsValue(JsonProperty property)
        => property.PropertyType.IsPrimitive || property.PropertyType.IsEnum;

    protected bool IsArray(JsonProperty property)
        => property.PropertyType.IsArray || typeof(IEnumerable).IsAssignableFrom(property.PropertyType);

    protected bool IsObject(JsonProperty property)
        //=> (IsClass(property) || IsStruct(property)) && !IsArray(property);
        => !IsValue(property) && !IsArray(property);

    //protected bool IsClass(JsonProperty property)
    //    => property.PropertyType.IsClass;

    //protected bool IsStruct(JsonProperty property)
    //    => property.PropertyType.IsValueType && !property.PropertyType.IsPrimitive && !property.PropertyType.IsEnum;

    protected T GetPropertyAttribute<T>(JsonProperty property) where T : Attribute
        => property.AttributeProvider
            .GetAttributes(typeof(T), false)
            .FirstOrDefault() as T;
}