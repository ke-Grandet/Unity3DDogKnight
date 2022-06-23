using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ConfigReader
{
    public static T ReadFile<T>(string configPath) where T : new()
    {
        //string fileAddress = System.IO.Path.Combine(Application.streamingAssetsPath, configPath);
        string strValue = File.ReadAllText(configPath);
        return JsonUtility.FromJson<T>(strValue);
        //FileInfo file = new(fileAddress);
        //if (file.Exists)
        //{
        //    StreamReader reader = new(fileAddress);
        //    string strValue = reader.ReadToEnd();
        //    T t = JsonUtility.FromJson<T>(strValue);
        //    reader.Close();
        //    return t;
        //}
        //else
        //{
        //    Debug.Log($"StreamingAssetsPath÷–≤ª¥Ê‘⁄{configPath}");
        //    return new T();
        //}
    }

    public static void SaveFile<T>(T t, string configPath)
    {
        string strJson = JsonUtility.ToJson(t, true);
        StreamWriter writer = new(configPath, false, System.Text.Encoding.UTF8);
        writer.Write(strJson);
        writer.Flush();
        writer.Close();
    }
}
