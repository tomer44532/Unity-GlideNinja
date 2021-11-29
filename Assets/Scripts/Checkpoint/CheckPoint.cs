using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField]
    private bool onEnter;

    [SerializeField]
    private bool onExit;

    [SerializeField]
    private float spawnY;
    private MapManager mapManager;
    // Start is called before the first frame update
    void Start()
    {
        mapManager = GameObject.Find("MapManager").GetComponent<MapManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (onEnter && col.gameObject.tag == "Player")
        {
            mapManager.SetRespawnDataCheckPoint(spawnY);
            enabled = false;
        }

    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (onExit && col.gameObject.tag == "Player")
        {
            mapManager.SetRespawnDataCheckPoint(spawnY);
            enabled = false;
        }

    }
}
