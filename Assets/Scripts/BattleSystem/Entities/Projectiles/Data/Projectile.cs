using UnityEngine;

public class Projectile : MonoBehaviour
{
    public ProjectileData data;

    [Header("Damage")]
    public int damage;
    int bonusDamage;
    
    [Header("Speed")]
    public float speed;
    float speedmultiplier;
    [Header("States")]
    public bool parried;



    private void Awake()
    {
        damage = data.baseDamage;
        speed = data.baseSpeed;
    }

    public virtual void Parry()
    {
        parried = true;
        gameObject.SetActive(false);
    }
    public virtual void TriggerDamage()
    {
        BattleHandler.Instance.currentPlayer.RecieveDamage(damage);
    }
   

}
