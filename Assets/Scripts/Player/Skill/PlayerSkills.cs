using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Skill : ISkill
{
    protected Player player;
    protected PlayerSkillSO skillData;

    public int skillId;
    protected int skillLevel;

    public virtual float Execute() 
    {
        return skillData.attackData.cooldown;
    }

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
    public Uppercut(Player player)
    {
        this.player = player;
        this.skillLevel = 1;

        for (int i = 0; i < player.skillDatas.Length; i++)
        {
            skillData = player.skillDatas[i];

            if (skillData.name == "Uppercut")
            {
                this.skillId = skillData.attackData.attackID;

                break;
            }
        }
    }

    public override float Execute()
    {
        if (!player.groundDetection.isGrounded || !player.stateMachine.isCancelable)
        {
            Debug.Log("Player is not Grounded!");
            return 0;
        }

        if (CooldownManager.Instance.SkillUsable("Uppercut", skillData.attackData.cooldown))
        {
            player.stateMachine.ChangeState(player.stateMachine.uppercutState);

            return skillData.attackData.cooldown;
        }
        else
        {
            Debug.Log("CoolTime!");
            return 0;
        }
    }
}

public class GatlingPunch : Skill
{
    public GatlingPunch(Player player)
    {
        this.player = player;
        this.skillLevel = 1;

        for (int i = 0; i < player.skillDatas.Length; i++)
        {
            skillData = player.skillDatas[i];

            if (skillData.name == "GatlingPunch")
            {
                this.skillId = skillData.attackData.attackID;

                break;
            }
        }
    }

    public override float Execute()
    {
        if (!player.groundDetection.isGrounded || !player.stateMachine.isCancelable)
        {
            Debug.Log("Player is not Grounded!");
            return 0;
        }

        if (CooldownManager.Instance.SkillUsable("GatlingPunch", skillData.attackData.cooldown))
        {
            player.stateMachine.ChangeState(player.stateMachine.gatlingPunchState);

            return skillData.attackData.cooldown;
        }
        else
        {
            Debug.Log("CoolTime!");
            return 0;
        }
    }
}
