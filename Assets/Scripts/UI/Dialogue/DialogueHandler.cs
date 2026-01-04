using System;
using UnityEngine;

public class DialogueHandler : MonoBehaviour
{
    static DialogueHandler _instance;
    public static DialogueHandler Instance
    {
        get 
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("DialogueHandler");
                go.AddComponent<DialogueHandler>();
            }
            return _instance; 
        }

    }

    public event Action<string[]> StartDialogue;
    public event Action DialogueStarted;
    public event Action DialogueEnded;

    private void Awake()
    {
        if(_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }
 
    public void SendDialogue(string[] dialogue)
    {
        StartDialogue?.Invoke(dialogue);
    }

    public void DialogueStart()
    {
        DialogueStarted?.Invoke();
        
    }
    public void DialogueEnd()
    {
        DialogueEnded?.Invoke();
    }
}
