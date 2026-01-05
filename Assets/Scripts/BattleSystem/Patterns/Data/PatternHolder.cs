using System;
using UnityEngine;

public class PatternHolder : MonoBehaviour
{

    [Header ("References")]
    public Enemy patternLauncher;
    public PlayerCursor playerCursor;

    [Header ("Base Info")]
    public bool patternOver = false;
    public float baseSpeed;
    public float baseDamage;

    [Header ("Modifiers")]
    public float speedMultiplier;
    public float damageMultiplier;

   
    public event Action<PatternHolder> patternFinished;


    private void Start()
    {
         StartPattern();
    }
    public virtual void StartPattern()
    {

    }

    public void EndPattern()
    {
        patternFinished.Invoke(this);
    }
}
