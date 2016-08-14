using UnityEngine;
using UnityEngine.SceneManagement;
using EntroMinds.Managers;

public class MainMenu : MonoBehaviour
{
	public string firstScene;
	public GameManager game;

	public void Play()
    {
		game.LoadLevel (firstScene);
    }

    //User clicked `Exit` button in-game
    public void Exit()
	{
		game.Quit ();
	}

//    public void Resume()
//    {   
//        gameObject.SetActive(false);
//        game.Resume();
//    }

    void LateUpdate()
    {
        if(game.isPaused() && Input.GetKey(KeyCode.Escape))
            gameObject.SetActive(false);
    }

	void OnLevelWasLoaded(int level)
    {
		if (SceneManager.GetActiveScene().name != "Main") {
			gameObject.SetActive (false);
		}
    }
}