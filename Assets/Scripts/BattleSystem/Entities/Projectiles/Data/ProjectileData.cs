using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileData", menuName = "Scriptable Objects/ProjectileData")]
public class ProjectileData : ScriptableObject
{
    public int baseDamage;
    public float baseSpeed;
    public string projectileName;
    public Projectile projectileObject;

}
