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
        // JSON 직렬화
        string jsonData = JsonConvert.SerializeObject(data);

        // 파일에 쓰기
        File.WriteAllText(filePath, jsonData);
    }

    public T LoadData()
    {
        if (File.Exists(filePath))
        {
            // 파일에서 데이터 읽기
            string jsonData = File.ReadAllText(filePath);

            // JSON 역직렬화
            return JsonConvert.DeserializeObject<T>(jsonData);
        }
        else
        {
            return default(T);
        }
    }
}
