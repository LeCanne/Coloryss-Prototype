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
          
    }

    void AddPlayer()
    {
        BattleHandler.Instance.SetCurrentPlayer(this);
    }

    public override void HasDied()
    {
        base.HasDied();
        Debug.Log("Annihilated. . .");
        BattleHandler.Instance.EndBattle();
        PatternHandler.Instance.InterruptPattern();
    }
    
}
