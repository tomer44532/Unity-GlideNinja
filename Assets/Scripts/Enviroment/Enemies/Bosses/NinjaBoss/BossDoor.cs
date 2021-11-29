using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoor : MonoBehaviour
{
    [SerializeField]
    private Sprite[] doorSprites;

    [SerializeField]
    private GameObject openDoorColliders;

    private NinjaBoss boss;

    public Sprite[] DoorSprites { get => doorSprites; set => doorSprites = value; }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Initialize(NinjaBoss ninjaBoss)
    {
        this.boss = ninjaBoss;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "normal projectile")
        {
            boss.TakeDamage();
        }
    }


    public void ChangeSprite(int index)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = DoorSprites[index];
    }

    public void OpenDoor()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.GetComponent<VerticalWall>().enabled = false;
        openDoorColliders.SetActive(true);

    }

    public void CloseDoor()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        gameObject.GetComponent<VerticalWall>().enabled = true;
        openDoorColliders.SetActive(false);

    }

}
