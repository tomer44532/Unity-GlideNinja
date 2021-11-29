using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColoredSpikes : ColoredObject
{
    [SerializeField]
    private GameObject[] spikesColliders;

    private Player player;

    private Animator animator;

    private int currentCollider;

    [SerializeField]
    private Sprite disabledSprite;

    public Player Player { get => player; set => player = value; }
    public int CurrentCollider { get => currentCollider; set => currentCollider = value; }


    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player").GetComponent<Player>();
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeColliders()
    {

        for (int i = 0; i < spikesColliders.Length; i++)
        {
            if (i == currentCollider)
            {
                spikesColliders[i].SetActive(true);
            }
            else
            {
                spikesColliders[i].SetActive(false);
            }
        }
    }

    public void disableColliders()
    {
        for (int i = 0; i < spikesColliders.Length; i++)
        {
            spikesColliders[i].SetActive(false);
        }
    }

    public override void enable()
    {
        Debug.Log("animation enabled " + gameObject.name+" " + transform.parent.transform.parent.gameObject.name);
        if (gameObject.activeInHierarchy)
        {
         
            base.enable();
            currentCollider = 0;
            changeColliders();
            animator.enabled = true;
            animator.Rebind();
            animator.Update(0f);
        }

    }

    public override void disable()
    {
        base.disable();
        //animator.SetTrigger("restartAnimation");
        animator.enabled = false;
        gameObject.GetComponent<SpriteRenderer>().sprite = disabledSprite;
        disableColliders();
    }
}
