using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColoredMovingObstacle : ColoredObstacle
{
    [SerializeField]
    private bool isMoving;

    [SerializeField]
    private GameObject arrows;

    [SerializeField]
    private float movementSpeed;

    private GameObject obstaclesPathsGameObject;
    private GameObject myPathGameObject;
    private List<GameObject> pathSpots;
    private int movingCurSpot;
    private int movingNextSpot;

    private bool movingRight = true;

    private bool movingUp = true;

    //private int direction = 1;
    private Vector3 direction;

    private float minX;
    private float maxX;
    private float minY;
    private float maxY;

    // Start is called before the first frame update
    void Start()
    {
        if (isMoving)
        {
            setMovingPath();
        }

    }

    private void setMovingPath()
    {
        obstaclesPathsGameObject = GameObject.Find("ObjectsMovementPath");
        myPathGameObject = obstaclesPathsGameObject.transform.Find(gameObject.name).gameObject;
        if (myPathGameObject.transform.childCount > 1)
        {
            pathSpots = new List<GameObject>();
            for (int i = 0; i < myPathGameObject.transform.childCount; i++)
            {
                pathSpots.Add(myPathGameObject.transform.GetChild(i).gameObject);
            }
            movingNextSpot = 1;
        }
        //transform.position = pathSpots[0].transform.position;
        direction = (pathSpots[movingNextSpot].transform.position - pathSpots[movingCurSpot].transform.position).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            Move();

        }
    }

    public void Move()
    {
        if(ColorEnabled)
        {

            if (pathSpots.Count > 1)
            {
                Debug.Log("move plate " + gameObject.name + " " + transform.position + " " + pathSpots[movingNextSpot].transform.position);
                if (transform.position == pathSpots[movingNextSpot].transform.position)
                {
                    movingCurSpot = movingNextSpot;
                    if (movingCurSpot == pathSpots.Count-1)
                    {
                        movingNextSpot = 0;
                    }
                    else
                    {
                        movingNextSpot++;
                    }
                    direction = (pathSpots[movingNextSpot].transform.position - pathSpots[movingCurSpot].transform.position).normalized;
                    Debug.Log(direction);

                }
            }


            transform.position = Vector2.MoveTowards(transform.position, pathSpots[movingNextSpot].transform.position, movementSpeed * Time.deltaTime);

        }
    }
}
