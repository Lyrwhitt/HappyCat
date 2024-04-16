using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill", menuName = "Skill/PlayerSkill")]
public class PlayerSkillSO : ScriptableObject
{
    [field: SerializeField] public PlayerAttackData attackData { get; private set; }
}
