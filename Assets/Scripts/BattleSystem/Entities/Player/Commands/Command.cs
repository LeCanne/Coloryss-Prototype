using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections;
using System;
using UnityEditor.Experimental.GraphView;


[RequireComponent (typeof(Button))]
public class Command : MonoBehaviour, ISelectHandler {

    public string commandName;
    public InputAction onCancel;
    public event Action commandDone;
    public AudioClip selectSound;
    public AudioClip useSound;
    public virtual void Awake()
    {
        onCancel = new InputAction();
        onCancel = InputSystem.actions.FindAction("Cancel");
        onCancel.performed += CancelAction;
        onCancel.Enable();
        TurnHandler.Instance.RegisterCommand(this);
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(DoCommand);
    }


    public virtual void DoCommand()
    {
       DoUsedSound();
        Debug.Log("Do This Command : " + commandName);
        TurnHandler.Instance.currentCommand = this;
    }

    public void CommandDone()
    {
        commandDone.Invoke();
    }

    public void CancelAction(InputAction.CallbackContext context)
    {
        if (context.performed && TurnHandler.Instance.currentCommand == this)
        {
            OnCancel();
        }
    }

    public virtual void OnCancel()
    {
        Debug.Log(commandName + " was Canceled");
    }

    public bool IsSelectable()
    {
        throw new NotImplementedException();
    }

    public void DoUsedSound() 
    {
        AudioHandler.Instance.SpawnClip(useSound, 0.6f, transform.position);
    }


    public void OnSelect(BaseEventData eventData)
    {
        AudioHandler.Instance.SpawnClip(selectSound,0.6f, transform.position);
    }
}
