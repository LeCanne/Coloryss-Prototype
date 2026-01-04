using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;


[RequireComponent (typeof(Button))]
public class Command : MonoBehaviour {

    public string commandName;
    public InputAction onCancel;
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
        Debug.Log("Do This Command : " + commandName);
        TurnHandler.Instance.currentCommand = this;
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
    
}
