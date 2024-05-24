using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineManager : MonoBehaviour
{
    private static CoroutineManager _instance;

    public static CoroutineManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject obj = new GameObject("CoroutineManager");
                _instance = obj.AddComponent<CoroutineManager>();
                DontDestroyOnLoad(obj);
            }
            return _instance;
        }
    }

    public static Coroutine StartCoroutineStatic(IEnumerator coroutine)
    {
        return Instance.StartCoroutine(coroutine);
    }

    public static void StopCoroutineStatic(Coroutine coroutine)
    {
        if (_instance != null)
        {
            Instance.StopCoroutine(coroutine);
        }
    }
}
