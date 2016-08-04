using UnityEngine;
using System.Collections;

public class AnimDelay : MonoBehaviour
{
    public float delay;
    private Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
        anim.enabled = false;
    }

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(delayAnimation());
	}

    public IEnumerator delayAnimation()
    {
        yield return new WaitForSeconds(delay);
        anim.enabled = true;
    }
}
