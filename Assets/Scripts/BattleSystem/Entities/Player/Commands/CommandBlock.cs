using UnityEngine;
using UnityEngine.Events;

public class CommandBlock : Command
{
    
    public override void Awake()
    {
        base.Awake();
        TurnHandler.Instance.SetFirstCommand(this);
        
    }
    public override void DoCommand()
    {
        base.DoCommand();

        Block();
       
        
    }

    void Block()
    {
        BattleHandler.Instance.currentPlayer.blocking = true;
        TurnHandler.Instance.EndTurn();
    }
}
