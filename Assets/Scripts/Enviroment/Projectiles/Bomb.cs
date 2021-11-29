using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Projectile
{
    private Animator animator;


    private float explodeAnimationLength;
    // Start is called before the first frame update
    protected override void Start()
    {
        animator = GetComponent<Animator>();
        GetAnimationsLength();
        Debug.Log("explode " + explodeAnimationLength);
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void OnCollisiontay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerGameObject.GetComponent<Player>().Actions.Death();
            Destroy(gameObject);
            return;
        }
        if(other.gameObject != dropingGameObject)
        {
            StartCoroutine(Explode(other));
        }
        //base.OnCollisiontay2D(other);
    }

    protected override void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerGameObject.GetComponent<Player>().Actions.Death();
            Destroy(gameObject);
            return;
        }
        if (other.gameObject != dropingGameObject)
        {
            Debug.Log("bomb drop " + other.gameObject.name + " " + dropingGameObject.name);
            StartCoroutine(Explode(other));
        }
        //base.OnCollisionEnter2D(other);
    }

    public IEnumerator Explode(Collision2D other)
    {
        animator.SetTrigger("explode");
        yield return new WaitForSeconds(explodeAnimationLength);
        Destroy(gameObject);

    }

    private void GetAnimationsLength()
    {
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;

        foreach (AnimationClip clip in clips)
        {
            switch (clip.name)
            {
                case "bomb exploding":
                    explodeAnimationLength = clip.length * animator.speed;
                    Debug.Log("explode " + animator.speed);
                    break;
            }
        }
    }

    public override void Initialize(GameObject dropingGameObject,Vector2 direction, MapManager mapManager, string color)
    {
        base.Initialize(dropingGameObject,direction, mapManager, color);
    }

    public new void OnDestroy()
    {
        base.OnDestroy();
    }
}
