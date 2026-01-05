
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;


public class TurnHandler : MonoBehaviour
{
    private static TurnHandler _instance;
    public static TurnHandler Instance
    {
        get 
        { 
            if((object)_instance == null)
            {
                GameObject go = new GameObject("TurnHandler");
                go.AddComponent<TurnHandler>();
            }
            
            return _instance; 
        }
    }

    public bool playerTurn { get; private set; }

    public Button defaultOption;
    public List<Button> Options = new List<Button>();
    public Command currentCommand;

    public event Action playerTurnBegin;
    public event Action enemyTurnBegin;
    int currentActiveEnemy = 0;
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }

        BattleHandler.Instance.battleStarted += InitializeBattleTurns;
        BattleHandler.Instance.battleFinished += ResetTurnHandler;
        BattleHandler.Instance.playerDied += ResetTurnHandler;
        

    }

    private void Start()
    {
        EventSystem.current.firstSelectedGameObject = defaultOption.gameObject;
    }

    public void InitializeBattleTurns()
    {
        ResetTurnHandler();
        PlayerTurn();
    }
    public void ResetTurnHandler()
    {
        
        BattleHandler.Instance.DisableEnemySelection();
        BattleHandler.Instance.RemoveInteractions();
        DisableCommands();
        playerTurn = true;


    }
    #region Command Handling
    public void SetFirstCommand(Command command)
    {
        defaultOption = command.GetComponent<Button>();
    }

    public void RegisterCommand(Command command)
    {
        Button btn = command.GetComponent<Button>();
        if (!Options.Contains(btn))
        {
            Options.Add(btn);
        }
        else
        {
            Debug.LogWarning(btn.name + " is already in the command List!");
        }
    }

    public void EnableCommands()
    {
        foreach (Button option in Options)
        {
            option.enabled = true;
        }
    }

    public void DisableCommands()
    {
        foreach (Button option in Options)
        {
            option.enabled = false;
        }
    }
    #endregion


    #region TurnOrder
    public void StartTurn()
    {

        if (playerTurn == true)
        {
            Debug.Log("PlayerTurn");
            PlayerTurn();
        }
        else
        {
            Debug.Log("EnemyTurn");
            EnemyTurn();
        }
    }

    public void EndTurn()
    {
        if(BattleHandler.Instance.currentEnemies.Count <= 0 && BattleHandler.Instance.currentPlayer.dead != true)
        {
            Debug.Log("Player Won");
            BattleHandler.Instance.EndBattle();
        }
        else if(BattleHandler.Instance.currentPlayer.dead != true)
        {
            playerTurn = !playerTurn;
            BattleHandler.Instance.DisableEnemySelection();
            BattleHandler.Instance.RemoveInteractions();
            DisableCommands();
            StartTurn();
        }
        
       
    }
    #endregion

    #region Turns
    void EnemyTurn()
    {
        currentActiveEnemy = 0;
        enemyTurnBegin?.Invoke();
        LaunchAttack();

    } 

   void PlayerTurn()
   {
        playerTurnBegin?.Invoke();
        EnableCommands();
        EventSystem.current.SetSelectedGameObject(defaultOption.gameObject);
   }
    #endregion


    void LaunchAttack()
    {
      
        Enemy enemy = BattleHandler.Instance.currentEnemies[currentActiveEnemy];
        currentActiveEnemy++;
        enemy.LaunchAttack();
        enemy.patternDone += EvaluatePatterns;
        
    }

    void EvaluatePatterns(Enemy enemy)
    {
        enemy.patternDone -= EvaluatePatterns;
        if(BattleHandler.Instance.currentEnemies.Count <= currentActiveEnemy)
        {
            EndTurn();
            
        }
        else
        {
            LaunchAttack();
          
        }
    }


    



}
