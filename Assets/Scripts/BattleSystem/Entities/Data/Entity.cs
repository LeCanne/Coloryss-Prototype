
using System;
using UnityEngine;
using Unity.UI;

public class Entity : MonoBehaviour, IDamageable
{
    public EntityData entityData;
    public int maxHp;
    public int hp;
    public string unitName;
    public event Action Died;
    public event Action damaged;
    public bool dead;
    

    public void InitializeUnit(EntityData myEntityData)
    {
        entityData = myEntityData;
        SetInfo();
    }

    void SetInfo()
    {
        maxHp = entityData.maxHp;
        hp = maxHp;
        unitName = entityData.name;
    }


    public void RecieveDamage(int damage)
    {
        hp -= damage;

        if(hp > maxHp)
        {
            hp = maxHp; 
        }
        else if (hp < 0)
        {
            hp = 0; 
        }

        damaged?.Invoke();
        CheckDead();
    }

    public void CheckDead()
    {
        if(dead == false && hp <= 0)
        {
            dead = true;
            HasDied();
            
        }
    }

    public virtual void HasDied()
    {
        Died?.Invoke();
    }
}
