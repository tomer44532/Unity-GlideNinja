using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject MenusManager;
    [SerializeField]
    private bool isTutorial;

    [SerializeField]
    private Scrollbar bgmScrollBar;

    [SerializeField]
    private Scrollbar sfxScrollBar;

    private SoundManager soundManager;

    public bool IsTutorial { get => isTutorial; set => isTutorial = value; }

    // Start is called before the first frame update
    void Start()
    {
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CloseMenu()
    {
        if (!isTutorial)
        {
            Debug.Log("pause");
            MenusManager.GetComponent<MenusManager>().CloseMenu("pause");
        }
        if (IsTutorial)
        {

        }

    }

    public void Quit()
    {
        MenusManager.GetComponent<MenusManager>().CloseMenu("pause");
        SceneManager.LoadScene("MainMenu");
    }

    public void MoveToTutorialStage()//used only in tutorial
    {
        GameObject.Find("TutorialManager").GetComponent<TutorialManager>().MoveToStage();
        MenusManager.GetComponent<MenusManager>().CloseMenu("pause");
        GameObject.Find("MapManager").GetComponent<MapManager>().SetRespawnData();
    }

    public void MoveToTutorialStageByKey()//used only in tutorial
    {
        GameObject.Find("TutorialManager").GetComponent<TutorialManager>().MoveToStage();
        GameObject.Find("MapManager").GetComponent<MapManager>().SetRespawnData();
    }

    public void ChangeBgmVolume()
    {
        soundManager.changeBgmVolume(bgmScrollBar.value);
    }

    public void ChangeSfxVolume()
    {
        soundManager.changeSfxVolume(sfxScrollBar.value);
    }


}
