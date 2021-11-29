using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuCommand : Command
{
    private MenusManager menusManager;

    public PauseMenuCommand(Player player, KeyCode key) : base(key)
    {
        menusManager = player.Components.MenusManager;

    }

    public override void GetKeyDown()
    {
        menusManager.OpenClosePauseMenu();
    }
}
