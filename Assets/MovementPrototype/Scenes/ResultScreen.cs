using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ResultScreen : MonoBehaviour
{
    public float restartTime = 3f;

    void Start()
    {
        StartCoroutine("RestartGame");
    }

    IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(restartTime);
        SceneManager.LoadScene(0);
    }
}
