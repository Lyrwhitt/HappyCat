using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager<T>
{
    private string filePath;

    public DataManager(string filePath)
    {
        this.filePath = filePath;
    }

    public void SaveData(T data)
    {
        string jsonData = JsonConvert.SerializeObject(data);

        File.WriteAllText(filePath, jsonData);
    }

    public T LoadData()
    {
        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);

            return JsonConvert.DeserializeObject<T>(jsonData);
        }
        else
        {
            return default(T);
        }
    }
}
