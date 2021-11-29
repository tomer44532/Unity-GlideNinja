using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalWall : MonoBehaviour
{
    private MapManager mapManager;

    [SerializeField]
    private bool chageToRight;
    // Start is called before the first frame update
    void Start()
    {
        mapManager = GameObject.Find("MapManager").GetComponent<MapManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.name == "Player")
        {

            Player player = col.gameObject.GetComponent<Player>();
            //if (player.Stats.MovingRight != chageToRight)
            //{
            //    player.Actions.Flip();
            //}
            mapManager.DirectionRight = chageToRight;
            player.Stats.MovingRight = chageToRight;
        }
    }
}
