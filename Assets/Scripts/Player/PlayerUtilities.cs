using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUtilities 
{
    private Player player;

    private List<Command> commands = new List<Command>();

    public PlayerUtilities(Player player)
    {
        this.player = player;

        commands.Add(new FallCommand(player,KeyCode.Space));
        commands.Add(new DashRight(player, KeyCode.RightArrow));
        commands.Add(new DashLeft(player, KeyCode.LeftArrow));
        commands.Add(new PauseMenuCommand(player, KeyCode.Escape));

    }


    public void HandleInput()
    {
        foreach (Command command in commands)
        {
            if (Input.GetKeyDown(command.Key))
            {
                command.GetKeyDown();
            }
            if (Input.GetKeyUp(command.Key))
            {
                command.GetKeyUp();
            }
            if (Input.GetKey(command.Key))
            {
                command.GetKey();
            }
        }
    }
}
