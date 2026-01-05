using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCursor : MonoBehaviour
{
    public Entity playerEntity;
    List<Projectile> parryables = new List<Projectile>();
    float staggerTime = 0.5f;
    InputAction parry;
    bool canParry = true;
    void Awake()
    {
        PatternHandler.Instance.cursorPosition = transform;
        playerEntity = BattleHandler.Instance.currentPlayer;
        parry = InputSystem.actions.FindAction("Parry");
        parry.Enable();
        parry.started += ParryInput;
    }

    private void OnDestroy()
    {
        parry.started -= ParryInput;
    }



    void ParryInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            if(canParry == true)
            {
                Parry();
            }
          
        }
    }

    void Parry()
    {
       
        if (parryables.Count > 0)
        {
            for(int i = parryables.Count-1; i >= 0; i--)
            {
                Debug.Log("Parried : " + parryables[i].name);
                parryables[i].HandleParry();
              
            }
        }
        else
        {

            StartCoroutine(Cooldown());
        }
    }

    public void TriggerProjectileDamage(Projectile projectile)
    {
        projectile.TriggerDamage();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Projectile>(out Projectile proj))
        {
            parryables.Add(proj);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Projectile>(out Projectile proj) == true)
        {
            if (proj.parried == false)
            { 
                TriggerProjectileDamage(proj);
            }
            parryables.Remove(proj);
        }
          
    }

    IEnumerator Cooldown()
    {
        Debug.Log("Staggered!");
        canParry = false;
        yield return new WaitForSeconds(staggerTime);
        canParry = true;
        yield return null;
    }
    
        
    
}
