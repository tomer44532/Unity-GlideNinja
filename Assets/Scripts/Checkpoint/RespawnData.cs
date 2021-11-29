using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnData 
{
    private Vector3 spawnPosition;
    private bool isRight;
    private bool bossActive;
    private bool greenActive;
    private bool blueActive;
    private float cameraMinY;
    private float cameraMaxY;


    public Vector3 SpawnPosition { get => spawnPosition; set => spawnPosition = value; }
    public bool IsRight { get => isRight; set => isRight = value; }
    public bool BossActive { get => bossActive; set => bossActive = value; }
    public bool GreenActive { get => greenActive; set => greenActive = value; }
    public float CameraMinY { get => cameraMinY; set => cameraMinY = value; }
    public float CameraMaxY { get => cameraMaxY; set => cameraMaxY = value; }
    public bool BlueActive { get => blueActive; set => blueActive = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
