using UnityEngine;

[CreateAssetMenu(fileName = "P_DATA_RotateOnce", menuName = "Scriptable Objects/ProjectileBehaviors/RotateTowards")]
public class RotateTowardsBehavior : ProjectileBehavior
{
    public override void DoBehavior(Projectile projectile)
    {
        var data = projectile.GetBehaviorData<RotateTowardsBehaviorData>(this);
        projectile.transform.right = data.directionToRotate;
    }
}
