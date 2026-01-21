using TMPro;
using UnityEngine;

public class PlayerBattleUI : MonoBehaviour
{
    public TMP_Text playerName;
    public TMP_Text playerHp;
    public TMP_Text txtCommandInfo;

    public Animator commandInfo;
    public Animator playerInfo;

    Entity playerUnit;
    void Awake()
    {
        BattleHandler.Instance.battleStarted += UIInitialize;
    }

    private void UIInitialize()
    {
       
        playerUnit = BattleHandler.Instance.currentPlayer;
        playerUnit.damaged += UpdatePlayerUI;
        UpdatePlayerUI();
    }

    void UpdatePlayerUI()
    {
        playerName.text = playerUnit.unitName;
        playerHp.text = playerUnit.hp.ToString();
    }

    void HidePlayerInfo()
    {
        playerInfo.SetTrigger("HidePlayer");
    }

    void ShowPlayerInfo()
    {
        playerInfo.SetTrigger("ShowPlayer");
    }

    void ShowCommandInfo(string commandInfoText)
    {
        txtCommandInfo.text = commandInfoText;
        commandInfo.SetTrigger("ShowCommand");
        
    }

    void HideCommandInfo()
    {
        commandInfo.SetTrigger("HideCommand");
    }
    

    public void DisplayInfo(string myCommandInfo)
    {
        ShowCommandInfo(myCommandInfo);
        HidePlayerInfo();
    }

    public void HideInfo()
    {
        HideCommandInfo();
        ShowPlayerInfo();
    }
}
