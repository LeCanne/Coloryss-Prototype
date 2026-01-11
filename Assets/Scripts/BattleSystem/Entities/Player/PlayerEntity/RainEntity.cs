using UnityEngine;

public class RainEntity : Entity
{
    private void Awake()
    {
        if(entityData != null)
        {
            InitializeUnit(entityData);
            AddPlayer();
        }
        else
        {
            Debug.LogWarning("No data has been set to player!");
        }

        TurnHandler.Instance.playerTurnBegin += DisableBlock;
    }

    void AddPlayer()
    {
        BattleHandler.Instance.SetCurrentPlayer(this);
    }

    public override void RecieveDamage(int damage)
    {
        if (blocking)
        {
            damage = Mathf.FloorToInt(damage / 2); 
        }
        base.RecieveDamage(damage);
    }

    public void DisableBlock()
    {
        blocking = false;
    }

    public override void HasDied()
    {
        base.HasDied();
        blocking = false;
        Debug.Log("Annihilated. . .");
      
        BattleHandler.Instance.EndBattle();
        PatternHandler.Instance.InterruptPattern();
        BattleHandler.Instance.SendBattleMessage("A N N I H I L A T E D . . .");
        
    }
    
}
