using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : Projectile,IColoredObject
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }


    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    //protected override void Update()
    //{
    //    base.Update();
    //}

    protected override void OnBecameInvisible()
    {
        Debug.Log("destroy");
        base.OnBecameInvisible();
    }

    protected override void OnCollisiontay2D(Collision2D other)
    {
        base.OnCollisiontay2D(other);
    }

    protected override void OnCollisionEnter2D(Collision2D other)
    {
        base.OnCollisionEnter2D(other);
    }

    public override void Initialize(GameObject throwingGameObject,Vector2 direction, MapManager mapManager, string color)
    {
        base.Initialize(throwingGameObject,direction,mapManager,color);
    }

    public void enable()
    {

    }

    public void disable()
    {
        Debug.Log("throw");
        Destroy(gameObject);
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
    }
}
