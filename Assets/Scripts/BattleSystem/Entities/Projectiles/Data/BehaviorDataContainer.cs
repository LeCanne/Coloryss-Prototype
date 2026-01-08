using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[System.Serializable]
public class BehaviorDataContainer
{
    [Description("ProjectileBehavior desired for this container")]
    [SerializeField] public List<ProjectileBehavior> behaviors;
    [Description("Handles how many seconds to elapse until behavior is done")]
    [SerializeField] [Min (0)] public float timeToComplete;
    [Description("Handles how many loops are necessary before  it's taken from the list \n 0: Loops Infinitely")]
    [SerializeField] [Min (0)] public int loops;

}
