using UnityEngine;

[CreateAssetMenu(fileName = "PatternData", menuName = "Scriptable Objects/PatternData")]
public class PatternData : ScriptableObject
{
    public PatternHolder patternObject;
    public string patternName;
}
