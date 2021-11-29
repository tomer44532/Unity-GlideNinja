using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWall : MonoBehaviour
{
    [SerializeField]
    private GameObject topWall;

    [SerializeField]
    private GameObject botWall;

    [SerializeField]
    private float movementSpeed;

    [SerializeField]
    private bool startMovingUp;

    [SerializeField]
    private float maxY;

    [SerializeField]
    private float minY;
    
    private bool movingUp;


    // Start is called before the first frame update
    void Start()
    {
        movingUp = startMovingUp;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Move()
    {
        if (movingUp)
        {

            transform.position = new Vector3(transform.position.x, transform.position.y + movementSpeed * Time.deltaTime, 0);
            if(transform.position.y  > maxY)
            {
                movingUp = false;
            }

        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - movementSpeed * Time.deltaTime, 0);
            if (transform.position.y < minY)
            {
                movingUp = true;
            }
        }
    }
}
