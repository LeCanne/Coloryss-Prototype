using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SelectArrow : MonoBehaviour
{
    private InputAction moveUI;
    bool updateArrowPos;
    private Image sprite;
    private void Awake()
    {
       moveUI = new InputAction();
       moveUI = InputSystem.actions.FindAction("NavigateDetect");
       moveUI.performed += OnNavigate;
       moveUI.started += OnNavigate;
       BattleHandler.Instance.enemySelection += UpdateArrow;
       BattleHandler.Instance.commandSelection += UpdateArrow;
       moveUI.Enable();
       sprite = GetComponent<Image>();

        TurnHandler.Instance.enemyTurnBegin += HideArrow;
        TurnHandler.Instance.playerTurnBegin += ShowArrow;
    }

    void OnNavigate(InputAction.CallbackContext context)
    {

        if (context.started)
        {
            UpdateArrow();
           
               
        }   
    }

    void UpdateArrow()
    {
        updateArrowPos = true;
    }

    void HideArrow()
    {
        sprite.enabled = false;
    }

    void ShowArrow()
    {
        sprite.enabled = true;
    }

    private void LateUpdate()
    {
        if (updateArrowPos == true && EventSystem.current.currentSelectedGameObject != null)
        {
            RectTransform go = EventSystem.current.currentSelectedGameObject.GetComponent<RectTransform>();
            RectTransform myRect = GetComponent<RectTransform>();


            Debug.Log(go.name);

            Vector3 arrowFinalpos = new Vector3(go.transform.position.x - 1, go.transform.position.y, 0);
            myRect.transform.position = arrowFinalpos;
            updateArrowPos = false;
        }
    }
}
