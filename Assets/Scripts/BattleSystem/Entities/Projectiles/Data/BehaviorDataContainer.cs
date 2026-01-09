using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditorInternal;
using UnityEngine;

[System.Serializable]
public class BehaviorDataContainer
{
    [Tooltip("ProjectileBehaviors desired for this container")]
    [SerializeField] public List<ProjectileBehavior> behaviors;
    [Tooltip("Handles how many seconds to elapse until behavior is done")]
    [SerializeField] [Min (0)] public float timeToComplete;
    [Tooltip("Handles how many loops are necessary before  it's taken from the list \n 0: Loops Infinitely")]
    [SerializeField] [Min (0)] public int loops;
  
}
