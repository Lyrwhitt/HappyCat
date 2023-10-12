using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "Characters/Player")]
public class PlayerSO : ScriptableObject
{
    [field: SerializeField] public PlayerGroundData groundedData { get; private set; }
    [field: SerializeField] public PlayerAirData airData { get; private set; }
    [field: SerializeField] public PlayerAttackData attackData { get; private set; }
}
