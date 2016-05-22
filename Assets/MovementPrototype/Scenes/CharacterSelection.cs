using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
    GameObject StartPrompt;
    GameObject[] playerAreaActive = new GameObject[4];
    GameObject[] playerAreaInactive = new GameObject[4];

    void Start()
    {
        StartPrompt = transform.Find("Panel").Find("Start Prompt").gameObject;

        playerAreaActive[0] = transform.Find("Panel").Find("Player 1 Area").Find("Active").gameObject;
        playerAreaActive[1] = transform.Find("Panel").Find("Player 2 Area").Find("Active").gameObject;
        playerAreaActive[2] = transform.Find("Panel").Find("Player 3 Area").Find("Active").gameObject;
        playerAreaActive[3] = transform.Find("Panel").Find("Player 4 Area").Find("Active").gameObject;

        playerAreaInactive[0] = transform.Find("Panel").Find("Player 1 Area").Find("Inactive").gameObject;
        playerAreaInactive[1] = transform.Find("Panel").Find("Player 2 Area").Find("Inactive").gameObject;
        playerAreaInactive[2] = transform.Find("Panel").Find("Player 3 Area").Find("Inactive").gameObject;
        playerAreaInactive[3] = transform.Find("Panel").Find("Player 4 Area").Find("Inactive").gameObject;
    }

    void Update()
    {
        PromptStart();
        ControlInput();
        StartInput();
    }

    void ControlInput()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            if (!playerAreaActive[0].activeSelf)
                Join(PlayerIndex.One);
        }
        else if (Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            if (playerAreaActive[0].activeSelf)
                Leave(PlayerIndex.One);
        }

        if (Input.GetKeyDown(KeyCode.Joystick2Button0))
        {
            if (!playerAreaActive[1].activeSelf)
                Join(PlayerIndex.Two);
        }
        else if (Input.GetKeyDown(KeyCode.Joystick2Button1))
        {
            if (playerAreaActive[1].activeSelf)
                Leave(PlayerIndex.Two);
        }

        if (Input.GetKeyDown(KeyCode.Joystick3Button0))
        {
            if (!playerAreaActive[2].activeSelf)
                Join(PlayerIndex.Three);
        }
        else if (Input.GetKeyDown(KeyCode.Joystick3Button1))
        {
            if (playerAreaActive[2].activeSelf)
                Leave(PlayerIndex.Three);
        }

        if (Input.GetKeyDown(KeyCode.Joystick4Button0))
        {
            if (!playerAreaActive[3].activeSelf)
                Join(PlayerIndex.Four);
        }
        else if (Input.GetKeyDown(KeyCode.Joystick4Button1))
        {
            if (playerAreaActive[3].activeSelf)
                Leave(PlayerIndex.Four);
        }
    }

    void StartInput()
    {
        if (StartPrompt.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Joystick1Button7))
            {
                if (playerAreaActive[0].activeSelf) StartGame();
            }

            if (Input.GetKeyDown(KeyCode.Joystick2Button7))
            {
                if (playerAreaActive[1].activeSelf) StartGame();
            }

            if (Input.GetKeyDown(KeyCode.Joystick3Button7))
            {
                if (playerAreaActive[2].activeSelf) StartGame();
            }

            if (Input.GetKeyDown(KeyCode.Joystick4Button7))
            {
                if (playerAreaActive[3].activeSelf) StartGame();
            }
        }
    }

    void PromptStart()
    {
        if (PlayerManager.GetPlayerList().Count > 1)
        {
            StartPrompt.SetActive(true);
        }
        else
        {
            StartPrompt.SetActive(false);
        }
    }

    void Join(PlayerIndex index)
    {
        int id = (int)index - 1;
        PlayerManager.AddPlayer(index);
        playerAreaActive[id].SetActive(true);
    }

    void Leave(PlayerIndex index)
    {
        int id = (int)index - 1;
        PlayerManager.RemovePlayer(index);
        playerAreaActive[id].SetActive(false);
    }

    void StartGame()
    {
        SceneManager.LoadScene("Loading");
    }
}
