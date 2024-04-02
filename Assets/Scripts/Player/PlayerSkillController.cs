using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillController : MonoBehaviour
{
    private Player player;

    private Dictionary<int, ICommand> skillDictionary = new Dictionary<int, ICommand>();

    public Uppercut uppercut;

    public ICommand qSkill;

    private void Awake()
    {
        player = GetComponent<Player>();

        InitPlayerSkill();
    }

    private void Start()
    {
        qSkill = skillDictionary[1001];
    }

    private void InitPlayerSkill()
    {
        uppercut = new Uppercut(this.player);
        skillDictionary.Add(uppercut.attackID, uppercut);
    }
}
