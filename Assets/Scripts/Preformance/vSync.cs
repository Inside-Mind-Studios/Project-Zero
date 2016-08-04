using UnityEngine;
using System.Collections;

public class vSync : MonoBehaviour
{
    private bool vsyncEnabled;
    private float inputDelay;
    private float timeSincePressed;

    void Awake()
    {
        DisableVSync();
        inputDelay = 0.5f;
        timeSincePressed = 0.0f;
    }

    void Update()
    {
        if (timeSincePressed >= inputDelay)
        {
            if (Input.GetKey(KeyCode.V) && Input.GetKey(KeyCode.LeftShift))
            {
                if (!vsyncEnabled)
                    EnableVSync();
                else
                    DisableVSync();

                timeSincePressed = 0.0f;
            }
        }
        timeSincePressed += Time.deltaTime;
    }

    public void EnableVSync()
    {
        QualitySettings.vSyncCount = 1;
        vsyncEnabled = true;
    }

    public void DisableVSync()
    {
        QualitySettings.vSyncCount = 0;
        vsyncEnabled = false;
    }
}
