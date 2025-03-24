using UnityEngine;

public static class JsonHelper
{
    public static T FromJson<T>(string json, bool newton = false)
    {
        if (newton)
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
        else return JsonUtility.FromJson<T>(json);
    }
    public static string ToJson<T>(T json, bool newton = false)
    {
        if (newton) return Newtonsoft.Json.JsonConvert.SerializeObject(json, Newtonsoft.Json.Formatting.Indented);
        else return JsonUtility.ToJson(json);
    }
    public static T[] FromJsonArray<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJsonArray<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJsonArray<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}