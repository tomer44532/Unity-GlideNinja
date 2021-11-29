using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField]
    private GameObject menusManagerGameObject;
    private int currentTutorialStage;

    [SerializeField]
    private GameObject[] tutorialPanels;

    [SerializeField]
    private GameObject[] tutorialStartPoints;

    [SerializeField]
    private GameObject[] tutorialObstacles;

    [SerializeField]
    private float spawnY;

    private Player player;

    private MapManager mapManager;

    public int CurrentTutorialStage { get => currentTutorialStage; set => currentTutorialStage = value; }

    // Start is called before the first frame update
    void Start()
    {
        menusManagerGameObject.GetComponent<MenusManager>().OpenMenu("pause");
        player = GameObject.Find("Player").GetComponent<Player>();
        mapManager = GameObject.Find("MapManager").GetComponent<MapManager>();
        ShowObstacles();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextStage()
    {
        tutorialPanels[CurrentTutorialStage].SetActive(false);
        CurrentTutorialStage++;
        tutorialPanels[CurrentTutorialStage].SetActive(true);
        ShowObstacles();
    }


    public void PrevStage()
    {
        tutorialPanels[CurrentTutorialStage].SetActive(false);
        CurrentTutorialStage--;
        tutorialPanels[CurrentTutorialStage].SetActive(true);
        ShowObstacles();

    }

    public void MoveToStage()
    {
        //player.Actions.Death();
        player.Actions.Respawn();
        player.transform.position = tutorialStartPoints[currentTutorialStage].transform.position;
        Debug.Log("move to stage");
        mapManager.RestoreActivators();
        ShowObstacles();

    }

    public void ShowObstacles()
    {
        for(int i = 0; i < tutorialObstacles.Length; i++)
        {
            if(i == currentTutorialStage)
            {
                tutorialObstacles[i].SetActive(true);
            }
            else
            {
                tutorialObstacles[i].SetActive(false);
            }
        }
    }
}
