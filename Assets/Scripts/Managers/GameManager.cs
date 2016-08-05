using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public LevelManager levelManager;
    public GameObject loadingSplash;
    private bool isGamePaused = false;

    private float loadProgress;

	// Use this for initialization
	void Start ()
    {
        //TODO...
	}
	
	// Update is called once per frame
	void Update ()
    {
        //TODO?...
	}

    public void Resume ()
    {
        Time.timeScale = 1;
        isGamePaused = false;
        Debug.Log("Game Resumed");
    }

    public void Pause()
    {
        Time.timeScale = 0;
        isGamePaused = true;
        Debug.Log("Game Paused");
    }

    public bool isPaused()
    {
        return isGamePaused;
    }

    public void LoadLevel(int index)
    {
		if (isGamePaused)
			Resume();
        levelManager.loadlevel(index);
        StartCoroutine(simulateLevelLoading(index));
        //loadingSplash.SetActive(true);
        //SceneManager.LoadScene(index);
    }

    public void LoadLevel(string levelName)
    {
        if (isGamePaused)
            Resume();
        levelManager.loadlevel(levelName);
        StartCoroutine(simulateLevelLoading(levelName));
    }

    void OnLevelWasLoaded(int level)
    {
        if (SceneManagerHelper.ActiveSceneName == "Main")
            setCursor(true);
        else
            setCursor(false);

        levelManager.levelloaded();
        //StartCoroutine(simulateLevelLoading());
        //loadingSplash.SetActive(false);
    }

    public void Quit()
    {
        // if we are running a stand alone build for the game
    #if UNITY_STANDALONE
        Application.Quit();
    #endif
        // if we are running the game through the editor
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #endif
    }

    public void setCursor(bool b)
    {
        Cursor.visible = b;
        if (b)
            Cursor.lockState = CursorLockMode.None;
        else
            Cursor.lockState = CursorLockMode.Confined;
    }

    /*
     * Coroutine to simulate the loading of a level
     * This requires to be commented out or deleted 
     * after it is no longer required
     */
    IEnumerator simulateLevelLoading(int index)
    {
        //Debug.Log("\"simulateLevelLoading\" Coroutine Started");

        //loadingSplash.SetActive(false);
        //Debug.Log("\"simulateLevelLoading\" Coroutine Ended");
        //yield return null;

        levelManager.progressBar.sizeDelta = new Vector2(loadProgress, 0);

        AsyncOperation async = SceneManager.LoadSceneAsync(index);
        while(!async.isDone)
        {
            loadProgress = (int)(async.progress * 100 + 60);
            levelManager.progressBar.sizeDelta = new Vector2(loadProgress, 0);
            yield return null;
        }
    }

    IEnumerator simulateLevelLoading(string sceneName)
    {
        //Debug.Log("\"simulateLevelLoading\" Coroutine Started");

        //loadingSplash.SetActive(false);
        //Debug.Log("\"simulateLevelLoading\" Coroutine Ended");
        //yield return null;

        levelManager.progressBar.sizeDelta = new Vector2(loadProgress, 0);

        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);
        while (!async.isDone)
        {
            loadProgress = (int)(async.progress * 100 + 60);
            levelManager.progressBar.sizeDelta = new Vector2(loadProgress, 0);
            yield return null;
        }
    }
}