using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollercoasterCart : MonoBehaviour
{
    [SerializeField]
    private float defaultMovementSpeed;

    [SerializeField]
    private Rollercoaster rollercoaster;

    private float curMovementSpeed;

    public float MovementSpeed { get => DefaultMovementSpeed; set => DefaultMovementSpeed = value; }
    public float CurMovementSpeed { get => curMovementSpeed; set => curMovementSpeed = value; }
    public float DefaultMovementSpeed { get => defaultMovementSpeed; set => defaultMovementSpeed = value; }
    public float DefaultZRotation { get => defaultZRotation; set => defaultZRotation = value; }

    private Vector3 prevPoint;

    private float prevAngle = 0;

    private float defaultZRotation;

    // Start is called before the first frame update
    void Start()
    {
        DefaultZRotation = transform.localEulerAngles.z;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializeCart()
    {
        RollercoasterPath rollercoasterPath = rollercoaster.RollercoasterPath;
        prevPoint = rollercoasterPath.GetFirstSpot();
        CurMovementSpeed = DefaultMovementSpeed;
    }

    public void Move()
    {
        if (((ColoredObject)rollercoaster).ColorEnabled)
        {
            Vector3 target = rollercoaster.RollercoasterPath.CheckNextSpot();
            if(rollercoaster.RollercoasterPath.MovingNextSpot != 0)
            {


                transform.position = Vector2.MoveTowards(transform.position, target, CurMovementSpeed * Time.deltaTime);
            }
            else //teleport to start
            {
                transform.position = rollercoaster.RollercoasterPath.GetFirstSpot();
            }
        }


        //SetAngle();
    }


    public void SetAngle()
    {
        //if(transform.position.x - prevPoint.x != 0)
        //{
        //    float m = (prevPoint.y - transform.position.y) / (prevPoint.x - transform.position.x);
        //    float angle = Mathf.Atan(m) * 180 / Mathf.PI;
        //    Debug.Log("rollercoaster " + m + " " + angle);
        //    transform.rotation = Quaternion.Euler(0, 0, angle);
        //}



        //if (prevPoint.x != transform.position.x)
        //{
        //    float m = (prevPoint.y - transform.position.y) / (prevPoint.x - transform.position.x);
        //    float angle = Mathf.Atan(m) * 180 / Mathf.PI;
        //    if ((transform.position.x < prevPoint.x))//&& angle < 0//m < 0 &&
        //    {
        //        angle = angle + 180;
        //    }
        //    angle = angle - prevAngle;
        //    transform.rotation = Quaternion.Euler(0, 0, angle);
        //    Debug.Log("rollercoaster " + m + " " + angle);

        //    //transform.Rotate(0,0,angle,Space.World);
        //    prevAngle = transform.rotation.z;
        //}

    }

}
