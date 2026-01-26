using TMPro;
using UnityEngine;

public class BattleInfoDisplay : MonoBehaviour
{
    public TMP_Text displayText;
    public Animator animator;
    private void Awake()
    {
        BattleHandler.Instance.sendBattleMessage += DisplayMessageInstantly;
        BattleHandler.Instance.commandSelection += HideInfoDisplay;
        PatternHandler.Instance.patternStarted += HideInfoDisplay;
        animator = GetComponent<Animator>();
    }
    void DisplayMessageInstantly(string message)
    {
        animator.SetBool("ShowDisplay", true);
        displayText.enabled = true;
        displayText.text = message;
        Debug.Log("DisplayMessage");
    }

    void HideInfoDisplay()
    {
        animator.SetBool("ShowDisplay", false);
        displayText.enabled = false;
        displayText.text = null;
        Debug.Log("HideDisplay");
    }

    
}
