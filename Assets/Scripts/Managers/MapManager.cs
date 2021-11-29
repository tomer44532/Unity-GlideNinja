using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    private bool directionRight = true;
    [SerializeField]
    private bool greenActive;

    [SerializeField]
    private bool blueActive;

    [SerializeField]
    private string bgmName;

    private List<GameObject> greens;
    private List<GameObject> blues;


    private GameObject[] activators;

    private Player player;
    private CameraFollow cameraFollow;
    private SoundManager soundManager;

    [SerializeField]
    private bool isTutorial;


    //private RespawnData respawnData;
    private bool respawnRightDirection = true;
    private bool bossActive;
    [SerializeField]
    private Boss boss;


    public bool DirectionRight { get => directionRight; set => directionRight = value; }
    public bool GreenActive { get => greenActive; set => greenActive = value; }
    public List<GameObject> Greens { get => greens; set => greens = value; }
    public bool RespawnRightDirection { get => respawnRightDirection; set => respawnRightDirection = value; }
    public bool BossActive { get => bossActive; set => bossActive = value; }
    public Boss Boss { get => boss; set => boss = value; }
    public bool IsTutorial { get => isTutorial; set => isTutorial = value; }
    public List<GameObject> Blues { get => blues; set => blues = value; }
    public bool BlueActive { get => blueActive; set => blueActive = value; }

    //public class RespawnData
    //{
    //    public bool greenActive = true;
    //}
    private RespawnData respawnData;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void Initialize()
    {
        greens = new List<GameObject>(GameObject.FindGameObjectsWithTag("green"));
        blues = new List<GameObject>(GameObject.FindGameObjectsWithTag("blue"));
        activators = GameObject.FindGameObjectsWithTag("activator");
        player = GameObject.Find("Player").GetComponent<Player>();
        cameraFollow = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        respawnData = new RespawnData();
        SetRespawnData();
        soundManager.CurrentBgm = bgmName;
        soundManager.PlayBgmMusic(bgmName);
        Debug.Log("blue " + blues.Count);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetRespawnData()
    {
        respawnData.BossActive = BossActive;
        respawnData.GreenActive = greenActive;
        respawnData.BlueActive = blueActive;
        respawnData.IsRight = directionRight;
        respawnData.SpawnPosition = player.transform.position;
        respawnData.CameraMinY = cameraFollow.YMin;
        respawnData.CameraMaxY = cameraFollow.YMax;
        player.Components.CameraManager.StopCameraChangers();
        Debug.Log("respawn right " + directionRight);

    }

    public void SetRespawnDataCheckPoint(float spawnY)
    {
        respawnData.BossActive = BossActive;
        respawnData.GreenActive = greenActive;
        respawnData.BlueActive = blueActive;
        respawnData.IsRight = directionRight;
        respawnData.SpawnPosition = new Vector3(player.transform.position.x,spawnY,0);
        respawnData.CameraMinY = cameraFollow.YMin;
        respawnData.CameraMaxY = cameraFollow.YMax;
        player.Components.CameraManager.StopCameraChangers();
        Debug.Log("respawn right " + directionRight);

    }

    public void Respawn()
    {
        RestoreActivators();
        player.gameObject.transform.position = respawnData.SpawnPosition;
        if (respawnData.GreenActive)
        {
            greenActive = false;
            EnableDisableColor("green");
        }
        if (respawnData.BlueActive)
        {
            blueActive = false;
            EnableDisableColor("blue");
        }
        BossActive = respawnData.BossActive;
        directionRight = respawnData.IsRight;
        player.Stats.MovingRight = directionRight;
        cameraFollow.YMin = respawnData.CameraMinY;
        cameraFollow.YMax = respawnData.CameraMaxY;
    }

    public void RestoreActivators()
    {
        Debug.Log("restore activators");
        foreach(GameObject activator in  activators){
            activator.SetActive(true);
        }
    }

    public bool GetColorActiveStatus(string colorType)
    {
        switch (colorType)
        {
            case "green":
                return greenActive;
                break;
            case "blue":
                return blueActive;
                break;
        }
        return false;
    }

    public List<GameObject> getColoredObjects(string colorType)
    {
        List<GameObject> coloredObjects = null;
        switch (colorType)
        {
            case "green":
                coloredObjects = greens;
                break;
            case "blue":
                coloredObjects = blues;
                break;
        }
        return coloredObjects;
    }

    public void EnableDisableColor(string colorType)
    {
        List<GameObject> coloredObjects = getColoredObjects(colorType);
        bool enabledStatus = GetColorActiveStatus(colorType);
        foreach (GameObject coloredObject in coloredObjects)
        {
            GameObject coloredParent = coloredObject.transform.parent.gameObject;
            Debug.Log("green enabled " + enabled);
            if (enabledStatus)
            {
                coloredParent.GetComponent<IColoredObject>().disable();
            }
            else
            {
                coloredParent.GetComponent<IColoredObject>().enable();
            }
        }
        switch (colorType)
        {
            case "green":
                greenActive = !greenActive;
                break;
            case "blue":
                blueActive = !blueActive;
                break;
        }
    }

    public void ResetBoss()
    {
        if (bossActive)
        {
            boss.InitializeBoss();
        }
    }
}
