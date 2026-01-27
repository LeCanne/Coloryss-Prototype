using UnityEngine;
using UnityEngine.Events;

public class BattleStarter : MonoBehaviour
{
    public UnityEvent onBattleStart;
    public BattleData battleData;
    public AudioClip Music;
    
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
        StartMusic(Music);
    }

    void StartMusic(AudioClip music)
    {
        AudioHandler.Instance.PlayMusic(music, 0.175f, true);
    }
   
}
