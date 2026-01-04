using UnityEngine;

[CreateAssetMenu(fileName = "P_DATA_FollowTarget", menuName = "Scriptable Objects/ProjectileBehaviors/FollowPlayer")]
public class FollowPlayer : ProjectileBehavior
{
    public override void DoBehavior(Projectile projectile)
    {
        projectile.rb2d.MovePosition(PatternHandler.Instance.cursorPosition.position);
    }
}
