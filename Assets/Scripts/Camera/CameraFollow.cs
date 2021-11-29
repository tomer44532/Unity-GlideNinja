using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
   // [SerializeField]
   // private float xMax;
    [SerializeField]
    private float yMax;

   // [SerializeField]
  //  private float xMin;
    [SerializeField]
    private float yMin;

    private Transform target;

    public float YMax { get => yMax; set => yMax = value; }
    public float YMin { get => yMin; set => yMin = value; }
    public GameObject FollowGameObject { get => followGameObject; set => followGameObject = value; }

    private GameObject followGameObject;
    private float timeElapsed;
    float lerpDuration = 200f;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(FollowGameObject == null)//follow player
        {
            transform.position = new Vector3(target.position.x, Mathf.Clamp(target.position.y, YMin, YMax), transform.position.z);

        }
        //else
        //{
        //    float followX = Mathf.Lerp(transform.position.x, FollowGameObject.transform.position.x, timeElapsed / lerpDuration);
        //    float followY = Mathf.Lerp(transform.position.y, FollowGameObject.transform.position.y, timeElapsed / lerpDuration);
        //    transform.position = new Vector3(followX,followY, transform.position.z);
        //    if(Mathf.Abs(transform.position.x - FollowGameObject.transform.position.x) < 1)
        //    {
        //        FollowGameObject = null;
        //    }

        //}

    }

    public bool FollowObject()//return true if finished
    {
        float followX = Mathf.Lerp(transform.position.x, FollowGameObject.transform.position.x, timeElapsed  / lerpDuration);
        float followY = Mathf.Lerp(transform.position.y, FollowGameObject.transform.position.y, timeElapsed  / lerpDuration);
        Debug.Log("camera follow " + timeElapsed);
        transform.position = new Vector3(followX, followY, transform.position.z);
        timeElapsed += Time.deltaTime;
        if (Mathf.Abs(transform.position.x - FollowGameObject.transform.position.x) < 1)
        {
            FollowGameObject = null;
            return true;
        }
        return false;
    }
}
