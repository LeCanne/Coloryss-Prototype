
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BattleHandler : MonoBehaviour
{
    private static BattleHandler _instance;
    public static BattleHandler Instance 
    {
        get 
        { 
            if((object)_instance == null) 
            {
                GameObject go = new GameObject("BattleHandler");
                go.AddComponent<BattleHandler>();

                
            }
            return _instance;
        }
    }

    public event Action<BattleData> battleStartInfo;
    public event Action battleStarted;
    public event Action battleFinished;
    public event Action enemySelection;
    public event Action commandSelection;
    public event Action playerskillissued;
    public Entity currentPlayer;
    public List<Enemy> currentEnemies = new List<Enemy>();
    public List<Enemy> deadEnemies = new List<Enemy>();
   
    private void Awake()
    {
        if (_instance == null && _instance != this)
        {
            _instance = this;
        }
        else
        {
            Destroy(_instance);
        }
    }

   
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void StartBattle(BattleData battleData)
    {
        battleStartInfo?.Invoke(battleData);
        battleStarted?.Invoke();
    }
    public void EndBattle()
    {
        if(currentPlayer.hp <= 0)
        {
            playerskillissued?.Invoke();
        }
        else
        {
            battleFinished?.Invoke();
        }
        CleanupBattleHandler();   
    }

    public void AddEnemy(Enemy enemy)
    {
        currentEnemies.Add(enemy);
    }

    public void KillEnemy(Enemy enemy)
    {
        currentEnemies.Remove(enemy);
        deadEnemies.Add(enemy);
    }

    public void SetCurrentPlayer(Entity entity)
    {
        currentPlayer = entity;
    }

    #region enemySelection
    public void EnableEnemySelection()
    {
        foreach(Enemy enemy in currentEnemies)
        {
            enemy.EnableSelection();    
        }
        enemySelection?.Invoke();
    }

    public void DisableEnemySelection()
    {
       
        foreach (Enemy enemy in currentEnemies)
        {
            enemy.DisableSelection();
        }
        commandSelection?.Invoke();
    }
    #endregion

    #region commandInteractions
    public void AddCommandInteraction(UnityAction action)
    {
        foreach (Enemy enemy in currentEnemies)
        {
            enemy.AddFunction(action);
        }
    }

    public void RemoveInteractions()
    {
        foreach(Enemy enemy in currentEnemies)
        {
            enemy.RemoveActions();
        }
    }

    #endregion
 
    void CleanupBattleHandler()
    {
        RemoveAllEnemies();
        DisableEnemySelection();
        RemoveInteractions();
    }

    void RemoveAllEnemies()
    {
        for (int i = currentEnemies.Count-1; i >= 0; i--)
        {
            Enemy enemy = currentEnemies[i];
            currentEnemies.RemoveAt(i);
            Debug.Log("DESTROYING" + enemy.entityData.name);
            Destroy(enemy.gameObject);
            
        }

        for (int i = deadEnemies.Count-1; i >= 0; i--)
        {
            Enemy enemy = deadEnemies[i];
            deadEnemies.RemoveAt(i);
            Debug.Log("DESTROYING" + enemy.entityData.name);
            Destroy(enemy.gameObject);
        }
    }
    
}
