using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RollercoasterPath 
{
    private Rollercoaster rollercoaster;

    private List<GameObject> pathSpots;
    private int movingCurSpot;
    private int movingNextSpot;
    private Vector3 movingDirection;
    private GameObject pathGameObject;

    public int MovingNextSpot { get => movingNextSpot; set => movingNextSpot = value; }
    public int MovingCurSpot { get => movingCurSpot; set => movingCurSpot = value; }

    public RollercoasterPath(Rollercoaster rollercoaster, GameObject pathGameObject)
    {
        this.pathGameObject = pathGameObject;
        this.rollercoaster = rollercoaster;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPath()
    {
        if (pathGameObject.transform.childCount > 1)
        {
            pathSpots = new List<GameObject>();
            for (int i = 0; i < pathGameObject.transform.childCount; i++)
            {
                pathSpots.Add(pathGameObject.transform.GetChild(i).gameObject);
            }
            MovingNextSpot = 1;
        }
       rollercoaster.CartGameObject.transform.position = pathSpots[0].transform.position;
        movingDirection = (pathSpots[MovingNextSpot].transform.position - pathSpots[MovingCurSpot].transform.position).normalized;
    }


    public Vector3 CheckNextSpot()
    {
        if (pathSpots.Count > 1)
        {
            if (rollercoaster.CartGameObject.transform.position == pathSpots[MovingNextSpot].transform.position)
            {
                RollercoasterCart cart = rollercoaster.CartGameObject.GetComponent<RollercoasterCart>();
                MovingCurSpot = MovingNextSpot;
                if (MovingCurSpot == pathSpots.Count - 1)
                {
                    MovingNextSpot = 0;
                    cart.CurMovementSpeed = cart.DefaultMovementSpeed;

                }
                else
                {
                    MovingNextSpot++;
                    {
                         cart.CurMovementSpeed = cart.CurMovementSpeed * rollercoaster.RollercoasterPath.GetPointSpeedMultiplyer();
                        Debug.Log("rollercoaster " + rollercoaster.RollercoasterPath.GetPointSpeedMultiplyer());

                    }
                }
                movingDirection = (pathSpots[MovingNextSpot].transform.position - pathSpots[MovingCurSpot].transform.position).normalized;
                SetCartAngle();
            }
        }
        Debug.Log("next spot " + pathSpots[MovingNextSpot]);
        return pathSpots[MovingNextSpot].transform.position;


    }

    private void SetCartAngle()
    {
        Vector3 curPos = pathSpots[MovingCurSpot].transform.position;
        Vector3 nextPos = pathSpots[MovingNextSpot].transform.position;
        if (curPos.x != nextPos.x)
        {
            float m = (curPos.y - nextPos.y) / (curPos.x - nextPos.x);
            float angle = Mathf.Atan(m) * 180 / Mathf.PI;
            if (( nextPos.x < curPos.x))//&& angle < 0//m < 0 &&
            {
                angle = angle + 180;
            }
            angle = angle + rollercoaster.transform.rotation.y;
            if (rollercoaster.transform.eulerAngles.y == 180)
            {
                angle = 180 - angle;
                Debug.Log("mirror " + angle);
                //rollercoaster.CartGameObject.transform.rotation = Quaternion.Euler(0, 0, angle);
                rollercoaster.CartGameObject.transform.localRotation = Quaternion.Euler(0, 0, angle);

            }
            else
            {
                //rollercoaster.CartGameObject.transform.rotation = Quaternion.Euler(0, 0, angle);
                rollercoaster.CartGameObject.transform.localRotation = Quaternion.Euler(0, 0, angle);
            }
        }
    }

    public Vector3 GetFirstSpot()
    {
        return pathSpots[0].transform.position;
    }

    public float GetPointSpeedMultiplyer()
    {
        return pathSpots[MovingCurSpot].GetComponent<RollercoasterSpeedChanger>().SpeedMultiply;
    }
}
