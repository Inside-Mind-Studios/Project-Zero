using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SliderLabeledVal : MonoBehaviour 
{
	public Graphics graphic;
	public Text text;
    public bool valueAsText;
    public string[] textValues;
	private Slider slider; 

	void Awake()
	{
		slider = GetComponent <Slider> ();
        if(valueAsText)
        {
            slider.wholeNumbers = true;
            slider.minValue = 0;
            slider.maxValue = textValues.Length - 1;
        }  
	}

	// Use this for initialization
	void Start () 
	{
		dislayValue ();
	}
		
	public void changeValueStr()
	{
		dislayValue ();
		graphic.setQualityLevel (slider.value);
	}

	private void dislayValue()
	{
        if (!valueAsText)
            text.text = slider.value.ToString();
        else
            text.text = textValues[(int)Mathf.Floor(slider.value)];
	}
}
