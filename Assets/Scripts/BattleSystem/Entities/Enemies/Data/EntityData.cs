using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EntityData")]
public class EntityData : ScriptableObject
{
    public new string name;
    public int maxHp;
    public Sprite sprite;
    public PatternData[] patterns;
}
