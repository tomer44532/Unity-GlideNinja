using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptimizationActivation : MonoBehaviour
{
    protected SpriteRenderer spriteRenderer;
    private MonoBehaviour[] components;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        components = GetComponents<MonoBehaviour>();


    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("optimization " + spriteRenderer.isVisible +" "+gameObject);
        //if (spriteRenderer.isVisible)
        //{
        //    gameObject.SetActive(true);
        //}
        //else
        //{
        //    gameObject.SetActive(false);
        //}
    }

    void OnBecameInvisible()
    {
        //gameObject.SetActive(false);
        foreach (MonoBehaviour c in components)
        {
            Debug.Log("optimization" + gameObject + " " + c);
            c.enabled = false;
        }
    }

    //void OnBecameVisible()
    //{
    //    gameObject.SetActive(true);
    //}

    void OnBecameVisible()
    {
        //enabled = true;
        foreach (MonoBehaviour c in components)
        {
            c.enabled = true;
        }
    }
}
