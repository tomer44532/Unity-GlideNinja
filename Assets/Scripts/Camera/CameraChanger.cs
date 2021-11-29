using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChanger : MonoBehaviour
{
    private CameraFollow cameraFollow;

    private Player player;

    private bool firstPass = true;

    private float prevMaxY;
    private float prevMinY;

    [SerializeField]
    private float curMaxY;

    [SerializeField]
    private float curMinY;

    private float goalMinY;
    private float goalMaxY;

    private bool toRight;

    private bool isChanging;

    private float timeElapsed;
    float lerpDuration = 5;

    [SerializeField]
    private bool changeMin;
    [SerializeField]
    private bool changeMax;

    public bool IsChanging { get => isChanging; set => isChanging = value; }
    public float TimeElapsed { get => timeElapsed; set => timeElapsed = value; }

    // Start is called before the first frame update
    void Start()
    {
        cameraFollow = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("checkpoint " + cameraFollow.YMin + " " + cameraFollow.YMax+" " +isChanging);
        Debug.Log("lerping!! " + IsChanging);

        if (IsChanging)
        {

            Debug.Log("lerping " + TimeElapsed);
                if (TimeElapsed < lerpDuration)
                {
                    if (changeMin)
                    {
                        cameraFollow.YMin = Mathf.Lerp(cameraFollow.YMin, goalMinY, TimeElapsed / lerpDuration);
                        TimeElapsed += Time.deltaTime;
                        if (cameraFollow.YMin == goalMinY)
                        {
                            IsChanging = false;

                    }
                }
                    if (changeMax)
                    {
                        cameraFollow.YMax = Mathf.Lerp(cameraFollow.YMax, goalMaxY, TimeElapsed / lerpDuration);
                        TimeElapsed += Time.deltaTime;
                        if (cameraFollow.YMax == goalMaxY)
                        {
                            IsChanging = false;

                    }
                }

                }
            
           
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.name == "Player")
        {
            if (firstPass)
            {
                firstPass = false;
                prevMinY = cameraFollow.YMin;
                prevMaxY = cameraFollow.YMax;
                if (col.gameObject.GetComponent<Rigidbody2D>().velocity.x > 0)
                {
                    toRight = true;
                }
                else
                {
                    toRight = false;
                }
            }
            float direction = col.gameObject.GetComponent<Rigidbody2D>().velocity.x;
            if ((direction > 0 && toRight) || (direction < 0 && !toRight))//move to right
            {
                Debug.Log("lerping!! " + curMinY);
                IsChanging = true;
                if (changeMin)
                {
                    goalMinY = curMinY;
                }
                if (changeMax)
                {
                    goalMaxY = curMaxY;
                }
                //cameraFollow.YMin = curMinY;
                //cameraFollow.YMax = curMaxY;
            }
            else
            {
                IsChanging = true;
                if (changeMin)
                {
                    goalMinY = prevMinY;
                }
                if (changeMax)
                {
                    goalMaxY = prevMaxY;
                }
                //cameraFollow.YMin = prevMinY;
                //cameraFollow.YMax = prevMaxY;
            }
        }

    }
}
