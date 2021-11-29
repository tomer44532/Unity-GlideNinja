using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private static Player instance;

    public static Player MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Player>();
            }
            return instance;
        }
    }


    [SerializeField]
    private PlayerStats stats;

    [SerializeField]
    private PlayerComponents components;

    private PlayerUtilities utilities;

    private PlayerActions actions;

    public PlayerComponents Components { get => components; }
    public PlayerStats Stats { get => stats; }
    public PlayerActions Actions { get => actions; }
    public PlayerUtilities Utilities { get => utilities; }


    // Start is called before the first frame update
    void Start()
    {
        actions = new PlayerActions(this);
        components.Start();
        utilities = new PlayerUtilities(this);
        //stats = new PlayerStats();
        stats.start(this);
        components.MapManager.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        utilities.HandleInput();
        actions.Move();
        actions.handleAnimation();
    }


    void OnCollisionEnter2D(Collision2D col)
    {
        switch (col.gameObject.tag)
        {
            case "obstacle":
                actions.bounce(col);
                break;
            case "damage":
                actions.Death();
                break;
        }
        //if (col.gameObject.tag == "obstacle")
        //{
        //    //StartCoroutine(actions.bounce(col));
        //    actions.bounce(col);
        //}
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.gameObject.tag)
        {
            case "damage":
                actions.Death();
                break;
        }
    }


}
