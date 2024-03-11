using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillController : MonoBehaviour
{
    private Player player;

    public Uppercut uppercut;

    public ICommand qSkill;

    private void Awake()
    {
        player = GetComponent<Player>();

        InitPlayerSkill();
    }

    private void Start()
    {
        qSkill = uppercut;
    }

    private void InitPlayerSkill()
    {
        uppercut = new Uppercut(this.player);
    }
}
