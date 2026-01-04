using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public TMP_Text playerName;
    public TMP_Text playerHp;

    Entity playerUnit;
    void Awake()
    {
        BattleHandler.Instance.battleStarted += UIInitialize;
    }

    private void UIInitialize()
    {
       
        playerUnit = BattleHandler.Instance.currentPlayer;
        playerUnit.damaged += UpdateUI;
        UpdateUI();
    }

    void UpdateUI()
    {
        playerName.text = playerUnit.unitName;
        playerHp.text = playerUnit.hp.ToString();
    }
    
}
