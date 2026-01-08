using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "P_DATA_FollowTarget", menuName = "Scriptable Objects/ProjectileBehaviors/FollowPlayer")]
public class LaunchToPlayer : ProjectileBehavior
{
  
    public override void DoBehavior(Projectile projectile)
    {       
          Transform target = PatternHandler.Instance.cursorPosition;
          Vector3 direction = target.transform.position - projectile.transform.position;
          projectile.rb2d.linearVelocity = direction.normalized * projectile.speed;         
    }

    
}
