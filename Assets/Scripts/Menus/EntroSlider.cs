﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EntroSlider : MonoBehaviour 
{
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
	}

	private void dislayValue()
	{
        if (!valueAsText)
            text.text = slider.value.ToString();
        else
            text.text = textValues[(int)Mathf.Floor(slider.value)];
	}
}
