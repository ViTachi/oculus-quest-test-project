using Newtonsoft.Json;
using System;
using System.IO;
using UnityEngine;

public static class RemoteConfigUtils
{
    /// <summary>
    /// Applies remote config data from json to the specified object.
    /// </summary>
    /// <param name="target">The target object to apply data.</param>
    /// <param name="json">The data of remote config.</param>
    public static void ApplyRemoteConfig(object target, string json)
    {
        if (string.IsNullOrEmpty(json)) return;

        var serializer = JsonSerializer.CreateDefault();
        serializer.ContractResolver = new RemoteConfigRuntimeContractResolver();

        try
        {
            using (var stream = new StringReader(json))
            using (var reader = new JsonTextReader(stream))
            {
                serializer.Populate(reader, target);
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to apply remote config. See details below.");
            Debug.LogException(e);
        }
    }
}

