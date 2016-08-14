using UnityEngine;
using System.Collections;

[System.Serializable]
public class Graphics
{
	void Start()
	{
        //Load saved settings from File	
	}

	public void increaseQualityLevel()
	{
		QualitySettings.IncreaseLevel ();
	}

	public void decreaseQualityLevel()
	{
		QualitySettings.DecreaseLevel ();
	}

	public void setQualityLevel(float level)
	{
		QualitySettings.SetQualityLevel ((int)level);
	}

    public void setAntiAliasing(int level)
    {
        QualitySettings.antiAliasing = level;
    }

    public void setShadows(ShadowResolution res)
    {
        QualitySettings.shadowResolution = res;
    }

    public void setVSync(bool b)
    {
        if (b)
            QualitySettings.vSyncCount = 1;
        else
            QualitySettings.vSyncCount = 0;
    }

    public void setTripleBuffering(bool b)
    {
        if (b)
            QualitySettings.maxQueuedFrames = 3;
        else
            QualitySettings.maxQueuedFrames = 0;
    }

    public void setScreenResolution(int values)
    {
        //Screen.SetResolution();
    }
}
