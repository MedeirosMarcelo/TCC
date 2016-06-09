using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class ResultOverlay : Menu
{
    public override void Start()
    {
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
                    SceneManager.LoadScene("Loading");
                    break;
                case 1:
                    SceneManager.LoadScene("Main Menu");
                    break;
                default:
                    break;
            }
        }
    }
}
