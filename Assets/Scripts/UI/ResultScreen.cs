using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class ResultScreen : MonoBehaviour
{
    public float restartTime = 3f;
    public Text resultText;

    void Start()
    {
        if (resultText && GameData.Winner)
        {
            resultText.text = "WINNER IS PLAYER " + GameData.Winner.id.ToString().ToUpper();
        }
        StartCoroutine("RestartGame");
    }

    IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(restartTime);
        SceneManager.LoadScene("Sword Prototype");
    }
}
