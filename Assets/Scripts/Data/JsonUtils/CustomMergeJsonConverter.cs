using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

internal class CustomMergeJsonConverter : JsonConverter
{
    private ICustomMergeStrategy _mergeStrategy;

    public CustomMergeJsonConverter(ICustomMergeStrategy mergeStrategy)
    {
        _mergeStrategy = mergeStrategy;
    }

    public override bool CanConvert(Type objectType) => false;

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        var token = JToken.Load(reader);
        var remoteValue = token.ToObject(objectType, serializer);

        return _mergeStrategy.Merge(existingValue, remoteValue);
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }
}
