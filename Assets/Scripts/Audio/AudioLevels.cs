using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioLevels : MonoBehaviour 
{
	public new AudioMixer audio;

	public void SetMasterLevel(float level)
	{
		if (level == -30)
			audio.SetFloat("Audio_Master_Volume", turnOff ());
		else
			audio.SetFloat("Audio_Master_Volume", level);
	}

	public void SetMusicLevel(float level)
	{
		if (level == -30)
			audio.SetFloat ("Audio_Music_Volume", turnOff ());
		else
			audio.SetFloat("Audio_Music_Volume", level);
	}
		
	public void SetSFXLevel(float level)
	{
		if (level == -30)
			audio.SetFloat ("Audio_SFX_Volume", turnOff ());
		else
			audio.SetFloat("Audio_SFX_Volume", level);
	}

	public void SetVoiceLevel(float level)
	{
		if (level == -30)
			audio.SetFloat ("Audio_Voice_Volume", turnOff ());
		else
			audio.SetFloat ("Audio_Voice_Volume", level);
	}

	private float turnOff()
	{
		return -80;
	}
}
