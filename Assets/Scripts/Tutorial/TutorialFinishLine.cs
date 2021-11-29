using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialFinishLine : MonoBehaviour
{
    [SerializeField]
    private bool moveToNext;
    [SerializeField]
    private int currentStage;

    private TutorialManager tutorialManager;
    private MenusManager menusManager;
    // Start is called before the first frame update
    void Start()
    {
        tutorialManager = GameObject.Find("TutorialManager").GetComponent<TutorialManager>();
        menusManager = GameObject.Find("MenusManager").GetComponent<MenusManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.name == "Player" &&tutorialManager.CurrentTutorialStage == currentStage)
        {
            if (moveToNext)
            {
                tutorialManager.NextStage();
                menusManager.OpenMenu("pause");               
            }
            else
            {
                menusManager.OpenMenu("pause");

            }
        }
    }
}
