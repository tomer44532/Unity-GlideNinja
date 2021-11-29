using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaEnemy : Enemy,IColoredObject
{
    [SerializeField]
    private Transform knifePos;

    [SerializeField]
    private GameObject knifePrefab;


    // Start is called before the first frame update
    public override void Start()
    {
        colorEnabled = true;
        base.Start(); 
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        float playerDistance = Vector3.Distance(player.transform.position, transform.position);
        if (colorEnabled && gameObject.GetComponent<SpriteRenderer>().isVisible && playerDistance > 10f )
        {
            Debug.Log("visisble enemy");
            ThrowKnife();
        }

    }

    protected void ThrowKnife()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackCoolDown)
        {

            //tmp.GetComponent<Shuriken>().Initialize(Vector2.left);
            canAttack = true;
            attackTimer = 0;
        }
        if (canAttack)
        {
            if (facingRight)
            {
                GameObject tmp = (GameObject)Instantiate(knifePrefab, knifePos.position, Quaternion.Euler(new Vector3(0, 0, -90)));
                tmp.GetComponent<Shuriken>().Initialize(gameObject,Vector2.right,mapManager,color);

            }
            else
            {

                GameObject tmp = (GameObject)Instantiate(knifePrefab, knifePos.position, Quaternion.Euler(new Vector3(0, 0, 90)));

                tmp.GetComponent<Shuriken>().Initialize(gameObject,Vector2.left,mapManager,color);
            }


            canAttack = false;
            animator.SetTrigger("throw");
        }
    }

    public void enable()
    {
        colorEnabled = true;
    }

    public void disable()
    {
        colorEnabled = false;
    }



}
