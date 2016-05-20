using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Options : Menu
{
    GameObject mainMenuScreen;

    public override void Start()
    {
        mainMenuScreen = transform.parent.Find("Main Menu").gameObject;
        base.Start();
    }

    public override void Update()
    {
        ActivateOption();
        base.Update();
    }

    void ActivateOption()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton0) || Input.GetKeyDown(KeyCode.Return))
        {
            switch (cursorIndex)
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    mainMenuScreen.SetActive(true);
                    this.gameObject.SetActive(false);
                    break;
                default:
                    break;
            }
        }
    }
}
