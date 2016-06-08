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

    private float previousXAxis;
    private float xAxis;

    public override void Update()
    {
        UpdateAxis();
        ActivateOption();
        base.Update();
    }

    void UpdateAxis()
    {
        previousXAxis = xAxis;
        xAxis = Input.GetAxis("HorizontalJoy");
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
