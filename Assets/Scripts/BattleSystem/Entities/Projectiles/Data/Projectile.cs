using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.InputSystem.OnScreen.OnScreenStick;

public class Projectile : MonoBehaviour
{
    public ProjectileData P_data;
    public List<ProjectileBehavior> P_Behaviors;

    [Header("References")]
    public Rigidbody2D rb2d;
    
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
        rb2d = GetComponent<Rigidbody2D>();
        InitializeData(P_data);
    }

    private void Update()
    {
        UseAllBehaviors();
    }

    #region Behaviors
    void UseAllBehaviors()
    {
        if (P_Behaviors.Count > 0)
        {
            for(int i = 0; i < P_Behaviors.Count; i++)
            {
                P_Behaviors[i].DoBehavior(this);
            }
        }
    }

    void AddBehavior(ProjectileBehavior pBehavior)
    {
        P_Behaviors.Add(pBehavior);
    }

    void RemoveBehavior(ProjectileBehavior pbehavior)
    {
        P_Behaviors.Remove(pbehavior);
    }
    #endregion

    public void InitializeData(ProjectileData data)
    {
        damage = data.baseDamage;
        speed = data.baseSpeed;
    }

    public virtual void HandleParry()
    {
        parried = true;
        gameObject.SetActive(false);
    }
    public virtual void TriggerDamage()
    {
        BattleHandler.Instance.currentPlayer.RecieveDamage(damage);
    }
   

}
