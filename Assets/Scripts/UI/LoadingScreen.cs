using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    Slider progressBar;

    void Start()
    {
        progressBar = transform.Find("Slider").GetComponent<Slider>();
        StartCoroutine("LoadLevelWithProgress", "Arena");
    }

    IEnumerator LoadLevelWithProgress(string levelToLoad)
    {
        var async = SceneManager.LoadSceneAsync(levelToLoad);
        while (!async.isDone)
        {
            if (progressBar) progressBar.value = async.progress;
            yield return null;
        }
    }
}
