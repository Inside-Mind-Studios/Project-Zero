using UnityEngine;
using System.Collections;

public class CameraFollowSun : MonoBehaviour {

    public Transform targetPos;

	// Use this for initialization
	void Start ()
    {
	    //Nothing todo here...
	}
	
	/** 
     * Set Camera to follow the Sun's x position.
     * The result is a panning effect that 
     * follows the sun as it sets and 
     * then pans to view the sun as
     * it rises.
     */
	void LateUpdate ()
    {
        gameObject.transform.LookAt(new Vector3(targetPos.position.x, 0f, targetPos.position.z));
	}
}
