using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : Menu
{
    GameObject optionScreen;
    AudioSource source;

    public override void Start() {
        Time.timeScale = 1;
        optionScreen = transform.parent.Find("Options").gameObject;
        source = transform.parent.parent.GetComponent<AudioSource>();
        AudioManager.Play(ClipType.MenuBGM, source);
        base.Start();
    }

    public override void Update() {
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
                    AudioManager.Play(ClipType.GUIStartGame, audioSource);
                    SceneManager.LoadScene("Character Selection");
                    break;
                case 1:
                    optionScreen.SetActive(true);
                    this.gameObject.SetActive(false);
                    break;
                case 2:
                    Application.Quit();
#if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
#endif
                    break;
                default:
                    break;
            }
        }
    }
}
