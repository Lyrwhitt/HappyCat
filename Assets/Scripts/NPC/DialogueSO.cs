using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "NPC", menuName = "NPC/Dialogue")]
public class DialogueSO : ScriptableObject
{
    [field: SerializeField] public DialogueData dialogueData { get; private set; }
}
