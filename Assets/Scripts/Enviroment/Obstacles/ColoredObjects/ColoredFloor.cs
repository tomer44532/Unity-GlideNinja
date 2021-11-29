using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColoredFloor : ColoredObstacle
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.name == "Player")
        {
            col.gameObject.GetComponent<Player>().Actions.Death();
        }
    }


    public override void enable()
    {
        base.enable();
        setSprite();
        if (gameObject.GetComponent<Collider2D>() != null)
        {
            gameObject.GetComponent<Collider2D>().isTrigger = false;
        }

    }

    public override void disable()
    {
        base.disable();
        setSprite();
        if (gameObject.GetComponent<Collider2D>() != null)
        {
            gameObject.GetComponent<Collider2D>().isTrigger = true;

        }

    }



    public override void setSprite()
    {
        if (ColorEnabled)
        {
            //gameObject.GetComponent<SpriteRenderer>().sprite = enabledSprite;
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1f);

        }
        else
        {
            //gameObject.GetComponent<SpriteRenderer>().sprite = disabledSprite;
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);

        }
    }
}
