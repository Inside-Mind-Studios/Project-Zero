using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

[System.Serializable]
public class LevelManager
{
    public GameObject splashScreen;
    public RectTransform progressBar;

    private float loadProgress;

    // Load a level using its name
    public void loadlevel(string levelName)
    {
        splashScreen.SetActive(true);
        //SceneManager.LoadScene(levelName);
    }

    // Load a level using its index
    public void loadlevel(int levelIndex)
    {
        splashScreen.SetActive(true);
        //SceneManager.LoadScene(levelIndex);
    }

    // Run each time a level was loaded
    public void levelloaded()
    {
        splashScreen.SetActive(false);
    }

    IEnumerator loadingprogress(int index)
    {
        progressBar.sizeDelta = new Vector2(loadProgress, 0);

        AsyncOperation async = SceneManager.LoadSceneAsync(index);

        yield return null;
    }
}