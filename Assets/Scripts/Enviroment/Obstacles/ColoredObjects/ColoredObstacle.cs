using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColoredObstacle : ColoredObject
{
    [SerializeField]
    private Sprite enabledSprite;

    [SerializeField]
    private Sprite disabledSprite;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public override void Update()
    {
        
    }

    public override void  enable()
    {
        base.enable();
        setSprite();
        if(gameObject.GetComponent<Collider2D>() != null)
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

    public virtual void setSprite()
    {
        if (ColorEnabled)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = enabledSprite;
            //gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1f);

        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = disabledSprite;
            //gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);

        }
    }
}
