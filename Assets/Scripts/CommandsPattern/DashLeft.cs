using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashLeft : Command
{
    private Player player;

    public DashLeft(Player player, KeyCode key) : base(key)
    {
        this.player = player;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void GetKeyDown()
    {
        player.Actions.dash(false);
    }
}
