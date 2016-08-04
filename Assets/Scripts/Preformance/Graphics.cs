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
}
