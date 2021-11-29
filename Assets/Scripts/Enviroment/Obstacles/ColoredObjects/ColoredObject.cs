using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ColoredObject : MonoBehaviour,IColoredObject
{
    [SerializeField]
    protected string color;

    protected SpriteRenderer spriteRenderer;

    private bool colorEnabled = true;

    public bool ColorEnabled { get => colorEnabled; set => colorEnabled = value; }

    // Start is called before the first frame update
    public virtual void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    public virtual void Update()
    {

    }

    public virtual void enable()
    {
        ColorEnabled = true;
    }

    public virtual void disable()
    {
        ColorEnabled = false;
    }
}
