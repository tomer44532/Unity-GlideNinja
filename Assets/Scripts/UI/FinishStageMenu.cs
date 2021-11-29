using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishStageMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject MenusManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void MoveToMainMenu()
    {
        MenusManager.GetComponent<MenusManager>().CloseMenu("finish menu");
        SceneManager.LoadScene("MainMenu");
    }
}
