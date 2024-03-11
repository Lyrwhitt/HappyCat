using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Uppercut : ICommand
{
    private Player player;

    public Uppercut(Player player)
    {
        this.player = player;
    }

    public void Execute()
    {
        player.stateMachine.ChangeState(player.stateMachine.uppercutState);
    }
}
