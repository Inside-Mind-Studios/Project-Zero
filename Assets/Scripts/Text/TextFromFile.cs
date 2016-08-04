using UnityEngine;
using UnityEngine.UI;

public class TextFromFile : MonoBehaviour
{
    public TextAsset textFile;
    private Text textComponent;

    void Awake()
    {
        textComponent = GetComponent<Text>();
    }

	// Use this for initialization
	void Start ()
    {
        textComponent.text = textFile.text;
	}
}
