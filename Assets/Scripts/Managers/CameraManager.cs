using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] cameraChangers;

    // Start is called before the first frame update
    void Start()
    {
       cameraChangers = GameObject.FindGameObjectsWithTag("cameraChanger");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StopCameraChangers()
    {
        for (int i = 0; i < cameraChangers.Length; i++)
        {
            cameraChangers[i].GetComponent<CameraChanger>().IsChanging = false;
            cameraChangers[i].GetComponent<CameraChanger>().TimeElapsed = 0;
        }
    }
}
