using UnityEngine;
using UnityEngine.UI;

public class FPS : MonoBehaviour
{
    private Text fpsTextComponent;
    //public Text frametimeTextComponent;
    private int frames;
    private float timeLapsed;
    private float fps;
	
    void Awake()
    {
        fpsTextComponent = GetComponent<Text>();
    }

	// Update is called once per frame
	void Update ()
    {
        frames++;
        timeLapsed += Time.deltaTime;

        if (timeLapsed >= 1.0)
        {
            fpsTextComponent.text = frames.ToString();
            //frametimeTextComponent.text = (timeLapsed / frames).ToString();
            frames = 0;
            timeLapsed = 0.0f;
        }
	}
}
