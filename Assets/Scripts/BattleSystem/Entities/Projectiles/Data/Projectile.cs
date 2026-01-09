using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.InputSystem.OnScreen.OnScreenStick;

public class Projectile : MonoBehaviour
{
    public ProjectileData P_data;
    public List<BehaviorDataContainer> P_Behaviors;

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
    public float currentBehaviorTime;
    public int currentBehaviorData;
    public int currentLoop;
    private Dictionary<ProjectileBehavior, object> runtimeData
     = new Dictionary<ProjectileBehavior, object>();


    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        InitializeData(P_data);
    }

    private void FixedUpdate()
    {       
       UseAllBehaviors();   
    }

    #region Behaviors

   
    //Handles getting the data of a given behavior and it's current values
    //System works by modifying a data class that handles all runtime values that the SO provides.
    public T GetBehaviorData<T>(ProjectileBehavior behavior)
        where T : new()
    {
        if (!runtimeData.TryGetValue(behavior, out var data))
        {
            data = new T();
            runtimeData.Add(behavior, data);
        }

        return (T)data;
    }

    public void SetBehaviorData<T>(
    ProjectileBehavior behavior,
    T data)
    {
        runtimeData[behavior] = data;
    }

    void UseAllBehaviors()
    {
        if (P_Behaviors.Count > 0)
        {
            //Resets the behavior indexer and adds 1 to the behavior LoopCount
            if (currentBehaviorData > P_Behaviors.Count-1)
            {
                currentBehaviorData = 0;
                currentLoop++;
            }

            currentBehaviorTime += Time.deltaTime;
            BehaviorDataContainer containers = P_Behaviors[currentBehaviorData];

            //Verify if the currentbehaviorTime has incremented above the requiredTime
            if(containers.timeToComplete < currentBehaviorTime)
            {
                DoNextBehavior(containers);
            }

            //As long as time goes, continue doing Behaviors
            for (int i = 0; i < containers.behaviors.Count; i++)
            {
                containers.behaviors[i].DoBehavior(this);
            }
        }
    }

    void DoNextBehavior(BehaviorDataContainer container)
    {
        
        //RemoveBehavior after a number of loops
        if(container.loops != 0 && container.loops == currentLoop)
        {
            RemoveBehavior(container);
        }
        currentBehaviorTime = 0;
        currentBehaviorData++;
    }

    public void AddBehavior(BehaviorDataContainer pBehavior)
    {
        P_Behaviors.Add(pBehavior);
    }

    public void RemoveBehavior(BehaviorDataContainer pbehavior)
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
