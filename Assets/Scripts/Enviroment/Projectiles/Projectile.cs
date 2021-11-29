using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    protected GameObject playerGameObject;

    protected MapManager mapManager;

    protected GameObject dropingGameObject;


    protected string color;

    [SerializeField]
    private float movementSpeed;

    private Rigidbody2D rigidbody;

    private Vector2 direction;

    public float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        playerGameObject = GameObject.Find("Player");
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    //protected virtual void Update()
    //{
    //    if (!gameObject.GetComponent<SpriteRenderer>().isVisible)
    //    {
    //        Destroy(gameObject);
    //        Debug.Log("throw");
    //    }
    //}

    protected virtual void FixedUpdate()
    {
        rigidbody.velocity = rigidbody.velocity + direction * MovementSpeed;

        Debug.Log("boss shuriken " + rigidbody.velocity);

    }

    protected virtual void OnBecameInvisible()
    {
        Destroy(gameObject);
    }



    protected virtual void OnCollisiontay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerGameObject.GetComponent<Player>().Actions.Death();
        }
        Destroy(gameObject);
    }


    protected virtual void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerGameObject.GetComponent<Player>().Actions.Death();
        }
        Destroy(gameObject);
    }

    public virtual void Initialize(GameObject dropingGameObject,Vector2 direction,MapManager mapManager,string color)
    {
        this.dropingGameObject = dropingGameObject;
        this.direction = direction;
        this.mapManager = mapManager;
        this.color = color;
        AddToMapManager();

    }

    public void AddToMapManager()
    {
        switch(color)
        {
            case "green":
                mapManager.Greens.Add(gameObject.transform.Find("green").gameObject);
                break;
            case "blue":
                mapManager.Blues.Add(gameObject.transform.Find("blue").gameObject);
                break;
        }
    }

    public void RemoveFromMapManager()
    {
        Debug.Log("green count " + mapManager.Greens.Count);
        switch (color)
        {
            case "green":
                mapManager.Greens.Remove(gameObject.transform.Find("green").gameObject);
                break;
            case "blue":
                mapManager.Blues.Remove(gameObject.transform.Find("blue").gameObject);
                break;
        }
        Debug.Log("green count " + mapManager.Greens.Count);

    }
    public virtual void OnDestroy()
    {
        Debug.Log("remove from list");
        RemoveFromMapManager();
    }


}
