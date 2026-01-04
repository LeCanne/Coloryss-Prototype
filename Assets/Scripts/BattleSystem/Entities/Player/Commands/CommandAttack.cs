using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent (typeof(Button))]
public class CommandAttack : Command
{
    public UnityAction dealDamage;
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
        SelectFirstEnemy();
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
    }

    void DealDamage()
    {
        GameObject currentEnemy = EventSystem.current.currentSelectedGameObject;
        Enemy targetedEnemy = currentEnemy.GetComponent<Enemy>();

        int dmgAmount = 5;
        Debug.Log("Deal " + dmgAmount +" to " +  targetedEnemy.name);
        targetedEnemy.RecieveDamage(dmgAmount);

        TurnHandler.Instance.EndTurn();
    }

   
}
