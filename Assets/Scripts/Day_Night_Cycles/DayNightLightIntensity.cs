using UnityEngine;
using System.Collections;

public class DayNightLightIntensity : MonoBehaviour {
    public Gradient lightColor;
    public float maxIntensity;
    public float minIntensity;
    public float minPoint;

    public float maxAmbient;
    public float minAmbient;
    public float minAmbientPoint;

    public Gradient fogColor;
    public AnimationCurve fogDensity;
    public float fogScale = 1f;

    public float atmosphere = 0.4f;

    Light light;
    Skybox sky;
    Material skyMaterial;

	// Use this for initialization
	void Start()
    {
        light = GetComponent<Light>();
        skyMaterial = RenderSettings.skybox;
	}
	
	// Update is called once per frame
	void Update()
    {
        calcIntensity();
        float dot = calcAmbience();
        light.color = lightColor.Evaluate(dot);
        //RenderSettings.ambientLight = light.color;
        //RenderSettings.fogColor = lightColor.Evaluate(dot);
        //RenderSettings.fogDensity = fogDensity.Evaluate(dot) * fogScale;
	}

    private float calcAmbience()
    {
        float tRange = 1 - minAmbientPoint;
        float dot = Mathf.Clamp01((Vector3.Dot(light.transform.forward, Vector3.down) - minPoint) / tRange);
        //float i = ((maxAmbient - minAmbient) * dot) + minAmbient;

        //RenderSettings.ambientIntensity = i;

        return dot;
    }

    private void calcIntensity()
    {
        float tRange = 1 - minPoint;
        float dot = Mathf.Clamp01((Vector3.Dot(light.transform.forward, Vector3.down) - minPoint) / tRange);
        float i = ((maxIntensity - minIntensity) * dot) + minIntensity;

        light.intensity = i;
    }
}
