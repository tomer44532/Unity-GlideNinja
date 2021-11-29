using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallCommand : Command
{
    private Player player;
    public FallCommand(Player player, KeyCode key) : base(key)
    {
        this.player = player;
    }

    public override void GetKeyDown()
    {
        player.Actions.StartFall();
    }

    public override void GetKeyUp()
    {
        player.Actions.StopFall();
    }

}
