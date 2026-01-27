using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CommandBlock : Command
{
    public PlayerBattleUI playerUI;
    public override void Awake()
    {
        base.Awake();
    }
    public override void DoCommand()
    {
        base.DoCommand();
        Block();  
    }

    void Block()
    {
        CommandDone();
       
        BattleHandler.Instance.currentPlayer.blocking = true;
        StartCoroutine(ShowCommand());
    }

    IEnumerator ShowCommand()
    {
        BattleHandler.Instance.SendBattleMessage("Rain braces for impact.");
        yield return new WaitForSeconds(2.5f);
        TurnHandler.Instance.ResolveTurn();
        yield return null;
    }
}
