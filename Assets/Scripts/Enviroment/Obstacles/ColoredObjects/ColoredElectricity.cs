using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColoredElectricity : ColoredObject
{
    [SerializeField]
    private Sprite disabledSprite;
    [SerializeField]
    private GameObject topPlate;
    [SerializeField]
    private GameObject botPlate;

    private Player player;

    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("lighting death " + animator.GetCurrentAnimatorStateInfo(0).IsName("Strike"));
        if (other.gameObject.tag == "Player"&& animator.enabled && animator.GetCurrentAnimatorStateInfo(0).IsName("Strike"))
        {
            player.Actions.Death();

        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && animator.enabled && animator.GetCurrentAnimatorStateInfo(0).IsName("Strike"))
        {
            player.Actions.Death();

        }
    }


    public override void enable()
    {
        base.enable();
        animator.enabled = true;
        animator.Rebind();
        animator.Update(0f);
        //Color color = animator.GetComponent<SpriteRenderer>().color;
        //color.a = 1;
        //animator.GetComponent<SpriteRenderer>().color = color;
    }

    public override void disable()
    {
        base.disable();
        //animator.SetTrigger("restartAnimation");
        animator.enabled = false;
        gameObject.GetComponent<SpriteRenderer>().sprite = disabledSprite;
        //Color color = animator.GetComponent<SpriteRenderer>().color;
        //color.a = 0;
        //animator.GetComponent<SpriteRenderer>().color = color;

    }
}
