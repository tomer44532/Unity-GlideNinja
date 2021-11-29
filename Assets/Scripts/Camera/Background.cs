using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private Rigidbody2D playerRigidbody;
    private GameObject player;
    void Awake()
    {
        //this.GetComponent<MeshRenderer>().sortingLayerName = "player";
    }
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerRigidbody = player.GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveBackground();
    }

    void MoveBackground()
    {
        //speed = myRigidbody.velocity.x;
        float playerSpeed = player.GetComponent<Player>().Stats.CurrentHorizontalSpeed;
        Vector2 offset = new Vector2( (Time.time*speed * playerSpeed), 0);//Time.time *
        Debug.Log("offset " + offset);
        print(speed);
        transform.position = new Vector2((Time.time * speed * playerSpeed), 0);
        //GetComponent<Renderer>().material.mainTextureOffset = offset;
    }
}
