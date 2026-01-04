using UnityEngine;
using UnityEngine.Events;

public class BattleStarter : MonoBehaviour
{
    public UnityEvent onBattleStart;
    public BattleData battleData;
    
    private void Awake()
    {
        BattleHandler.Instance.battleStartInfo += StartBattle;
    }

    private void Start()
    {
        BattleHandler.Instance.StartBattle(battleData);
    }
    void StartBattle(BattleData battleData)
    {
        onBattleStart.Invoke();
    }

   
}
