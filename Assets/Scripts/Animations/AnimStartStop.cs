using UnityEngine;
using System.Collections;

public class AnimStartStop : MonoBehaviour
{
    private Animator anim;
    private AnimDelay delay;

    void Awake()
    {
        anim = GetComponent<Animator>();
        if (!gameObject.activeInHierarchy)
            anim.enabled = false;
    }

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void LateUpdate ()
    {
        if (!gameObject.activeInHierarchy)
            anim.enabled = false;
    }
}
