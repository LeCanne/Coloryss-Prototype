using UnityEngine;
using UnityEngine.UI;

public class PatternArea : MonoBehaviour
{
    Image patternImage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        patternImage = GetComponent<Image>();
        PatternHandler.Instance.RegisterPatternArea(gameObject);

        PatternHandler.Instance.patternStarted += EnablePatternImage;
        PatternHandler.Instance.patternStopped += DisablePatternImage;
    }

    void EnablePatternImage()
    {
        patternImage.enabled = true;
    }

    void DisablePatternImage()
    {
        patternImage.enabled = false;
    }



    
}
