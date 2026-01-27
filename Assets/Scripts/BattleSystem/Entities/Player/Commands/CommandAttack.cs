using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System;

[RequireComponent (typeof(Button))]
public class CommandAttack : Command
{
    public UnityAction dealDamage;
    [TextArea(1,2)]public string commandDescription;
    public PlayerBattleUI playerUI;
    
    public float latenceAfterDamage;
    public float latenceAfterResolution;
    public AudioClip attackSound;
    public override void Awake()
    {
        base.Awake();
        TurnHandler.Instance.SetFirstCommand(this);
        dealDamage += DealDamage;
    }
    public override void DoCommand()
    {
        base.DoCommand();
        TurnHandler.Instance.DisableCommands();
        playerUI.DisplayInfo(commandDescription);
        SelectFirstEnemy();
        playerUI.HidePlayer();
    }

    void SelectFirstEnemy()
    {
        Enemy firstEnemy = BattleHandler.Instance.currentEnemies[0];
        BattleHandler.Instance.EnableEnemySelection();
        BattleHandler.Instance.AddCommandInteraction(dealDamage);
        EventSystem.current.SetSelectedGameObject(firstEnemy.gameObject);   
    }

    public override void OnCancel()
    {
        base.OnCancel();
        BattleHandler.Instance.DisableEnemySelection();
        BattleHandler.Instance.RemoveInteractions();
        EventSystem.current.SetSelectedGameObject(gameObject);
        TurnHandler.Instance.currentCommand = null;
        TurnHandler.Instance.EnableCommands();
        playerUI.HideInfo();
        playerUI.DisplayPlayerInfo();
    }

    void DealDamage()
    {
        AudioHandler.Instance.SpawnClip(attackSound, 0.4f, transform.position);
        CommandDone();
        DoUsedSound();
        StartCoroutine(DamageProcess());
        
    }

    IEnumerator DamageProcess()
    {
        GameObject currentEnemy = EventSystem.current.currentSelectedGameObject;
        Enemy targetedEnemy = currentEnemy.GetComponent<Enemy>();
        int dmgAmount = 5;
        Debug.Log("Deal " + dmgAmount + " to " + targetedEnemy.name);
        targetedEnemy.RecieveDamage(dmgAmount);
        BattleHandler.Instance.SendBattleMessage("You strike " + targetedEnemy.unitName + " for " +  dmgAmount + " damage.");
        yield return new WaitForSeconds(latenceAfterResolution);
        TurnHandler.Instance.ResolveTurn();
        yield return null;
    }

    

   
}
