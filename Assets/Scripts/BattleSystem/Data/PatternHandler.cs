using System;
using UnityEngine;

public class PatternHandler : MonoBehaviour
{
    private static PatternHandler _instance;
    public static PatternHandler Instance
    {
        get
        {
            if ((object)_instance == null)
            {
                GameObject go = new GameObject("PatternHandler");
                go.AddComponent<PatternHandler>();


            }
            return _instance;
        }
    }

    public PatternHolder currentPattern;
    private GameObject patternArea;
    public Transform cursorPosition; 

    public event Action patternStarted;
    public event Action patternStopped;
    private void Awake()
    {

        if (_instance == null && _instance != this)
        {
            _instance = this;
        }
        else
        {
            Destroy(_instance);
        }
    }

    public void RegisterPatternArea(GameObject patternZone)
    {
        patternArea = patternZone;
    }

    public Vector3 GetPatternAreaPosition()
    {
        return patternArea.transform.position;
    }

    public void StartPattern(PatternData patternData, Enemy enemy)
    {
        
        patternStarted?.Invoke();
        SpawnPattern(patternData, enemy);
    }

    public void EndPattern(PatternHolder pattern)
    {

        patternStopped?.Invoke();
        Destroy(pattern.gameObject);
        
        
    }

    void SpawnPattern(PatternData pattern, Enemy enemy)
    {
        PatternHolder currentPatternHolder = pattern.patternObject;
        GameObject newPatternHolder = Instantiate(currentPatternHolder.gameObject);

        newPatternHolder.GetComponent<PatternHolder>().patternLauncher = enemy;
        newPatternHolder.GetComponent<PatternHolder>().transform.position = gameObject.transform.position;
        newPatternHolder.GetComponent<PatternHolder>().patternFinished += EndPattern;

        currentPattern = newPatternHolder.GetComponent<PatternHolder>();

    }

    public void InterruptPattern()
    {
        if (currentPattern != null)
        {
            currentPattern.patternFinished -= EndPattern;
            EndPattern(currentPattern);
        }
    }
}
