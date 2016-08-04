using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour
{
    public int titleSceneIndex;
    public int creditsSceneIndex;
    private bool titleScene;
    private bool creditsScene;
    private GameObject mainMenu;
	private GameObject activeMenu;

    public GameObject titleMenu;
    public GameObject pauseMenu;
	public GameManager game;

	// Use this for initialization
	void Start()
    {
		titleMenu.SetActive (true);
//		if (titleScene)
//        {
//			titleMenu.SetActive (true);
//			activeMenu = titleMenu;
//			mainMenu = titleMenu;
//		}
	}

    void LateUpdate()
	{
		Debug.Log ("TitleScene: " + titleScene + " " + "CreditsScene: " + creditsScene);
		if (!titleScene && !creditsScene && Input.GetKeyDown(KeyCode.Escape))
		{
			if(game.isPaused ())
			{
				activeMenu.SetActive (false);
				game.Resume ();
			}
			else
			{
				game.Pause ();
				pauseMenu.SetActive (true);
				activeMenu = pauseMenu;
			}
        }
    }

    void OnLevelWasLoaded(int level)
    {
        switch(level)
        {
			case 1:
			{
				titleMenuIsMain();
				break;
			}
            case 3:
            {
                creditsScene = true;
                break;
            }
            default:
            {
                pauseMenuIsMain();
                break;
            }
        }   
    }

	public void GoToMenu(GameObject menu)
	{
		menu.SetActive (true);
		activeMenu = menu;
	}

	public void TurnMenuOff(GameObject menu)
	{
		Debug.Log ("menu is off");
		menu.SetActive (false);
	}

	public void GoToMainMenu()
	{
		mainMenu.SetActive (true);
		activeMenu = mainMenu;
	}

    private void pauseMenuIsMain()
    {
        titleScene = false;
        creditsScene = false;
        mainMenu = pauseMenu;
    }

    private void titleMenuIsMain()
    {
		if(!titleMenu.activeSelf) {
			titleMenu.SetActive(true);
			Debug.Log ("Title Menu was originally not active");
		}
			
		Debug.Log ("Title menu active");
        titleScene = true;
        creditsScene = false;
        mainMenu = titleMenu;
        activeMenu = mainMenu;
    }
}