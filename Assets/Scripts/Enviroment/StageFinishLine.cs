using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageFinishLine : MonoBehaviour
{
    private MenusManager menusManager;
    // Start is called before the first frame update
    void Start()
    {
        menusManager = GameObject.Find("MenusManager").GetComponent<MenusManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("finish line " + col.gameObject.name);
        if (col.gameObject.name == "Player")
        {
            menusManager.OpenMenu("finish menu");
        }
    }
}
