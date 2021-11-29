using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlayerComponents
{
    [SerializeField]
    private Rigidbody2D rigidbody;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private GameObject mapManagerGameObject;

    [SerializeField]
    private GameObject uiManagerGameObject;

    [SerializeField]
    private GameObject menusManagerGameObject;

    [SerializeField]
    private GameObject cameraManagerGameObject;

    [SerializeField]
    private GameObject soundManagerGameObject;

    private MapManager mapManager;
    private UIManager uiManager;
    private MenusManager menusManager;
    private CameraManager cameraManager;
    private SoundManager soundManager;

    public Rigidbody2D Rigidbody { get => rigidbody; set => rigidbody = value; }
    public Animator Animator { get => animator; set => animator = value; }
    public GameObject MapManagerGameObject { get => mapManagerGameObject; set => mapManagerGameObject = value; }
    public MapManager MapManager { get => mapManager; set => mapManager = value; }
    public UIManager UiManager { get => uiManager; set => uiManager = value; }
    public MenusManager MenusManager { get => menusManager; set => menusManager = value; }
    public CameraManager CameraManager { get => cameraManager; set => cameraManager = value; }
    public SoundManager SoundManager { get => soundManager; set => soundManager = value; }

    public void Start()
    {
        mapManager = mapManagerGameObject.GetComponent<MapManager>();
        uiManager = uiManagerGameObject.GetComponent<UIManager>();
        menusManager = menusManagerGameObject.GetComponent<MenusManager>();
        cameraManager = cameraManagerGameObject.GetComponent<CameraManager>();
        soundManager = soundManagerGameObject.GetComponent<SoundManager>();

    }
}
