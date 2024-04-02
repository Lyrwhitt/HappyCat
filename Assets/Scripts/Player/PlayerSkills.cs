using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Uppercut : ICommand
{
    private Player player;

    public int attackID;

    public Uppercut(Player player)
    {
        this.player = player;

        for(int i = 0; i < player.skillDatas.Length; i++)
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
        player.stateMachine.ChangeState(player.stateMachine.uppercutState);
    }
}
