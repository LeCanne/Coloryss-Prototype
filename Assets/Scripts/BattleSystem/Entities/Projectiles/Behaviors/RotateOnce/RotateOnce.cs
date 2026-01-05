using UnityEngine;

[CreateAssetMenu(fileName = "P_DATA_RotateOnce", menuName = "Scriptable Objects/ProjectileBehaviors/RotateOnce")]
public class RotateOnce : ProjectileBehavior
{
    public override void DoBehavior(Projectile projectile)
    {
        Transform target = PatternHandler.Instance.cursorPosition;
        projectile.transform.right = target.position - projectile.transform.position;
    }
}
