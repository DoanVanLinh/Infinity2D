using UnityEngine;
using System.IO;
using System.Collections.Generic;

public static class SaveLoadJson
{

    public static void Create<T>(string fileName, T data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(Application.dataPath + "/" + fileName, json);
    }
    public static void Create<T>(string fileName, T[] data)
    {
        int length = data.Length;
        string json = "";
        for (int i = 0; i < length; i++)
            json += JsonUtility.ToJson(data[i], true);
        File.WriteAllText(Application.dataPath + "/" + fileName, json);
    }

    public static void Update<T>(string fileName, T data)
    {
        string json = File.ReadAllText(Application.dataPath + "/" + fileName);
        JsonUtility.FromJsonOverwrite(json, (T)data);
    }
    public static T Load<T>(string fileName)
    {
        string json = File.ReadAllText(Application.dataPath + "/" + fileName);
        return JsonUtility.FromJson<T>(json);
    }
    public static T[] LoadArray<T>(string fileName)
    {
        string json = File.ReadAllText(Application.dataPath + "/" + fileName);
        json = json.Replace('{','}');
        string []arrayString =  json.Split('}');
        List<T> result = new List<T>();
        foreach (var item in arrayString)
        {
            if(item.Equals("")) continue;
            string convertToJson = "{"+item+"}";
            result.Add(JsonUtility.FromJson<T>(convertToJson));
        }
        return result.ToArray();
    }
}

