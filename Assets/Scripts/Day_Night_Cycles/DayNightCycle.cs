using UnityEngine;
using System.Collections;

public class DayNightCycle : MonoBehaviour {
    public float cycleSpeed;

	// Use this for initialization
	void Start ()
    {
	    // Nothing todo here...
	}
	
	// Update is called once per frame
	void Update ()
    {
        gameObject.transform.RotateAround(Vector3.zero, Vector3.right, cycleSpeed * Time.deltaTime);
        //transform.LookAt(Vector3.zero);
	}
}
