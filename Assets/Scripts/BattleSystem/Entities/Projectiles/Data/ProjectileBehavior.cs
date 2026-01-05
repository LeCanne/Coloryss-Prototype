using UnityEngine;


public class ProjectileBehavior : ScriptableObject
{
    public bool doOnce;
   public virtual void DoBehavior(Projectile projectile)
   {
        Debug.LogWarning("No behavior attached to object");
   }
}
