using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Linq;

internal class JoinMergeJsonConverter : JsonConverter
{
    public override bool CanConvert(Type objectType) => false;

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        var token = JToken.Load(reader);
        return MergeToken(token, objectType, existingValue, serializer);
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }

    public object MergeToken(JToken token, Type objectType, object existingValue, JsonSerializer serializer)
    {
        switch (token.Type)
        {
            case JTokenType.Array: return MergeArray(token as JArray, objectType, existingValue, serializer);
            case JTokenType.Object: return MergeObject(token, objectType, existingValue, serializer);
            default: return MergeValue(token, objectType, existingValue, serializer);
        }
    }

    public object MergeArray(JArray token, Type objectType, object existingValue, JsonSerializer serializer)
    {
        Type itemType;
        if (objectType.IsArray) itemType = objectType.GetElementType();
        else if (objectType.IsGenericType) itemType = objectType.GetGenericArguments().Single();
        else throw new NotSupportedException($"Type {objectType} is not supported.");

        var dst = existingValue as IList;

        if (dst.Count != token.Count)
            throw new IndexOutOfRangeException($"Number of items in remote array '{token.Path}' is not equal to capacity of local array.");

        for (int i = 0; i < token.Count; ++i)
        {
            var itemToken = token[i];
            dst[i] = MergeToken(itemToken, itemType, dst[i], serializer);
        }

        return existingValue;
    }

    public object MergeObject(JToken token, Type objectType, object existingValue, JsonSerializer serializer)
    {
        serializer.Populate(token.CreateReader(), existingValue);
        return existingValue;
    }

    public object MergeValue(JToken token, Type objectType, object existingValue, JsonSerializer serializer)
    {
        return token.ToObject(objectType, serializer);
    }
}

