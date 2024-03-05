using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Status", menuName = "Characters/Status")]
public class StatusSO : ScriptableObject
{
    public float maxHP;
    public float hp;
    public float atk;
    public float def;
}
