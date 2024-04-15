using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Skill : ICommand
{
    public int skillId;
    protected int skillLevel;

    public virtual void Execute() { }

    public void SetSkillLevel(int skillLevel)
    {
        this.skillLevel = skillLevel;
    }

    public int GetSkillLevel()
    {
        return skillLevel;
    }
}

public class Uppercut : Skill
{
    private Player player;

    //public int attackID;
    //private int skillLevel;

    public Uppercut(Player player)
    {
        this.player = player;
        this.skillLevel = 1;

        for (int i = 0; i < player.skillDatas.Length; i++)
        {
            PlayerSkillSO skillData = player.skillDatas[i];

            if (skillData.name == "Uppercut")
            {
                this.skillId = skillData.attackData.attackID;

                break;
            }
        }
    }

    public override void Execute()
    {
        if (!player.groundDetection.isGrounded)
        {
            Debug.Log("Player is not Grounded!");
            return;
        }

        player.stateMachine.ChangeState(player.stateMachine.uppercutState);
    }
}

public class GatlingPunch : Skill
{
    private Player player;

    public GatlingPunch(Player player)
    {
        this.player = player;
        this.skillLevel = 1;

        for (int i = 0; i < player.skillDatas.Length; i++)
        {
            PlayerSkillSO skillData = player.skillDatas[i];

            if (skillData.name == "GatlingPunch")
            {
                this.skillId = skillData.attackData.attackID;

                break;
            }
        }
    }

    public override void Execute()
    {
        if (!player.groundDetection.isGrounded)
        {
            Debug.Log("Player is not Grounded!");
            return;
        }

        player.stateMachine.ChangeState(player.stateMachine.gatlingPunchState);
    }
}
