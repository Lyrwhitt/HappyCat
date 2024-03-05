using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Effect
{
    public string tag;
    public GameObject prefab;
}

[CreateAssetMenu(fileName = "Effect", menuName = "Effect/Data")]
public class EffectSO : ScriptableObject
{
    public List<Effect> effects;
}