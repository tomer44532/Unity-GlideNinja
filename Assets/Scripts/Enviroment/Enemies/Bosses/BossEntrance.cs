using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEntrance : MonoBehaviour
{
    [SerializeField]
    private Boss boss;

    [SerializeField]
    private Sprite entranceSprite;
    [SerializeField]
    private Sprite wallSprite;

    private MapManager mapManager;
    // Start is called before the first frame update
    void Start()
    {
        mapManager = GameObject.Find("MapManager").GetComponent<MapManager>();
    }

    //public void Initialize(Boss boss)
    //{
    //    this.boss = boss;
    //}

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerExit2D(Collider2D col)
    {
        gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        gameObject.GetComponent<SpriteRenderer>().sprite = wallSprite;
        boss.gameObject.SetActive(true);
        mapManager.BossActive = true;
        enabled = false; 
    }
}
