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
        TurnHandler.Instance.enemyTurnBegin += DisplayPlayerInfo;
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

    #region AnimControls
    void HidePlayerInfo()
    {
        playerInfo.SetTrigger("HidePlayer");
    }

    void ShowPlayerInfo()
    {
        playerInfo.SetTrigger("ShowPlayer");
    }


    void HideCommandInfo()
    {
        commandInfo.SetTrigger("HideCommand");
    }

    void ShowCommandInfo(string commandInfoText)
    {
        txtCommandInfo.text = commandInfoText;
        commandInfo.SetTrigger("ShowCommand");

    }
    #endregion AnimControls

    #region HideWindow
    public void HideAllInfo()
    {
        HideCommandInfo();
        HidePlayerInfo();
    }

    public void HidePlayer()
    {
        HidePlayerInfo();
    }

    public void HideInfo()
    {
        HideCommandInfo();
    }
    #endregion HideWindow

    #region ShowWindow
    public void DisplayInfo(string myCommandInfo)
    {
        ShowCommandInfo(myCommandInfo);
      
    }

    public void DisplayPlayerInfo()
    {
        ShowPlayerInfo();
    }
    #endregion ShowWindow



}
