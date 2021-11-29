using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenusManager : MonoBehaviour
{
    private bool isPauseMenuOpen;

    [SerializeField]
    private GameObject pauseMenu;

    [SerializeField]
    private GameObject finishStageMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenClosePauseMenu()
    {
        if (!isPauseMenuOpen)//open menu
        {
            OpenMenu("pause");
            //pauseMenu.GetComponent<Image>().color = new Color(1,1,1,1);
        }
        else// close menu
        {
            CloseMenu("pause");
            //pauseMenu.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        }
    }

    public void OpenMenu(string menuName)
    {
        switch (menuName)
        {
            case "pause":
                isPauseMenuOpen = true;
                Time.timeScale = 0;
                pauseMenu.SetActive(true);
                break;
            case "finish menu":
                Time.timeScale = 0;
                finishStageMenu.SetActive(true);
                break;
        }
    }

    public void CloseMenu(string menuName)
    {
        switch (menuName)
        {
            case "pause":
                if (pauseMenu.GetComponent<PauseMenu>().IsTutorial)
                {
                    pauseMenu.GetComponent<PauseMenu>().MoveToTutorialStageByKey();
                }
                isPauseMenuOpen = false;
                Time.timeScale = 1;
                pauseMenu.SetActive(false);
                break;
            case "finish menu":
                Time.timeScale = 1;
                finishStageMenu.SetActive(false);
                break;
        }
    }
}
