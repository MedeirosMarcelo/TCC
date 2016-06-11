using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Options : Menu
{
    GameObject mainMenuScreen;
    Text master;
    Text sfx;
    Text music;

    public override void Start()
    {
        mainMenuScreen = transform.parent.Find("Main Menu").gameObject;
        master = transform.Find("Master").Find("Value").GetComponent<Text>();
        sfx = transform.Find("Sfx").Find("Value").GetComponent<Text>();
        music = transform.Find("Music").Find("Value").GetComponent<Text>();

        master.text = PlayerPrefs.GetInt("Master", 100).ToString();
        sfx.text = PlayerPrefs.GetInt("Sfx", 100).ToString();
        music.text = PlayerPrefs.GetInt("Music", 100).ToString();

        base.Start();
    }

    public override void Update()
    {
        ActivateOption();
        ChangeOption();
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

    void ChangeOption()
    {
        if (PressedRight() || Input.GetKeyDown(KeyCode.RightArrow))
        {
            switch (cursorIndex)
            {
                case 0:
                    SetVolumeOption(10, "Master", master);
                    break;
                case 1:
                    SetVolumeOption(10, "Sfx", sfx);
                    break;
                case 2:
                    SetVolumeOption(10, "Music", music);
                    break;
                case 3:
                    break;
                default:
                    break;
            }
        }

        if (PressedLeft() || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            switch (cursorIndex)
            {
                case 0:
                    SetVolumeOption(-10, "Master", master);
                    break;
                case 1:
                    SetVolumeOption(-10, "Sfx", sfx);
                    break;
                case 2:
                    SetVolumeOption(-10, "Music", music);
                    break;
                default:
                    break;
            }
        }
    }

    void SetVolumeOption(int fraction, string prefKey, Text textObj)
    {
        int val = Mathf.Clamp(PlayerPrefs.GetInt(prefKey) + fraction, 0, 100);
        PlayerPrefs.SetInt(prefKey, val);
        textObj.text = val.ToString();
    }
}
