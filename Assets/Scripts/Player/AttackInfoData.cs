using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AttackInfoData
{
    [field: SerializeField] public string attackName;
    [field: SerializeField] public int comboStateIndex;
    [field: SerializeField][field: Range(0f, 1f)] public float comboTransitionTime;
    [field: SerializeField][field: Range(0f, 3f)] public float forceTransitionTime;
    [field: SerializeField][field: Range(-10f, 30f)] public float force;
    [field: SerializeField][field: Range(0f, 1f)] public float drag;
    [field: SerializeField] public bool isCancelable;

    [field: SerializeField] public int damage;
}

[Serializable]
public class PlayerAttackData
{
    [field: SerializeField] public List<AttackInfoData> AttackInfoDatas;
    public int GetAttackInfoCount()
    {
        return AttackInfoDatas.Count;
    }

    public AttackInfoData GetAttackInfo(int index)
    {
        return AttackInfoDatas[index];
    }
}
