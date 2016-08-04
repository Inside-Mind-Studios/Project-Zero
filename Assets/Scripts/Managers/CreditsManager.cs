using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsManager : MonoBehaviour
{
    private Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }
	
	void Update ()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Complete"))
            SceneManager.LoadScene(1);
    }
}
