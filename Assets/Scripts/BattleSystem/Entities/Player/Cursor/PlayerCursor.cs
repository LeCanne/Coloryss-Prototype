
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCursor : MonoBehaviour
{

    [Header ("Data")]
    public Entity playerEntity;
    public Animator myAnimator;
    public GameObject spriteObject;
   
    List<Projectile> parryables = new List<Projectile>();
    float staggerTime = 0.5f;
    InputAction parry;

    [Header ("Sounds")]
    public AudioClip parrySound;
    public AudioClip failedParrySound;
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
                myAnimator.SetTrigger("Parry");
                parryables[i].HandleParry();
                AudioHandler.Instance.SpawnClip(parrySound, 0.5f, transform.position);
              
            }
        }
        else
        {

            StartCoroutine(Cooldown());
            StartCoroutine(Shake(1f,3f,staggerTime * 0.5f));
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
        AudioHandler.Instance.SpawnClip(failedParrySound, 0.5f, transform.position);
        myAnimator.SetTrigger("Staggered");
        Debug.Log("Staggered!");
        canParry = false;
        
        yield return new WaitForSeconds(staggerTime);
        myAnimator.SetTrigger("UnStagger");
        canParry = true;
        yield return null;
    }

    IEnumerator Shake(float shakeDistance, float shakeAmount, float shakeTime)
    {
        Vector3 shakeForce = Vector3.zero;
        float minshake = -shakeDistance;
        float maxshake = shakeDistance;

        while (shakeTime > 0)
        {
            shakeTime -= Time.deltaTime;
            
            float currentShakeX = Random.Range(minshake, maxshake);
            float currentShakeY = Random.Range(minshake, maxshake);

            spriteObject.transform.localPosition = new Vector3(currentShakeX, currentShakeY);
            
           
            yield return null;
        }
        spriteObject.transform.localPosition = Vector3.zero; 

        yield return null;
    }
    
        
    
}
