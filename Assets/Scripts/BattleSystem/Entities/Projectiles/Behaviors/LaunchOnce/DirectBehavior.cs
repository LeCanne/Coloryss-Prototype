using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "P_DATA_FollowTarget", menuName = "Scriptable Objects/ProjectileBehaviors/DirectBehavior")]
public class DirectBehavior : ProjectileBehavior
{
  
    public override void DoBehavior(Projectile projectile)
    {       
          var data = projectile.GetBehaviorData<DirectBehaviorData>(this);
          Vector3 direction = data.direction;
          projectile.rb2d.linearVelocity = direction.normalized * projectile.speed;
       
    }

    
}
