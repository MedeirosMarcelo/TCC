using UnityEngine;
using System.Collections;

public class PauseOverlayController : MonoBehaviour {

    GameObject pausePanel;
    bool pause = false;

    void Start() {
        pausePanel = transform.Find("Panel").gameObject;
    }

	void Update () {
        Control();
	}

    void Control()
    {
        if (UnityEngine.Input.GetKeyDown(KeyCode.JoystickButton7) ||
            UnityEngine.Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pause)
            {
                Pause(true);
            }
            else
            {
                Pause(false);
            }
        }
    }

    public void Pause(bool paused)
    {
        pausePanel.SetActive(paused);
        if (paused)
        {
            Time.timeScale = 0;
            pause = true;
        }
        else
        {
            Time.timeScale = 1;
            pause = false;
        }
    }
}
