using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Uppercut : ICommand
{
    private Player player;

    public int attackID;
    public int skillLevel;

    public Uppercut(Player player)
    {
        this.player = player;
        skillLevel = 0;

        for (int i = 0; i < player.skillDatas.Length; i++)
        {
            PlayerSkillSO skillData = player.skillDatas[i];

            if (skillData.name == "Uppercut")
            {
                attackID = skillData.attackData.attackID;

                break;
            }
        }
    }

    public void Execute()
    {
        if (!player.controller.isGrounded)
            return;

        player.stateMachine.ChangeState(player.stateMachine.uppercutState);
    }
}
