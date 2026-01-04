using System.Collections;
using TMPro;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using System;
using UnityEngine.UI;

public class DialogueDisplayer : MonoBehaviour
{
    InputAction passDialogue;
    public TMP_Text textBox;
    [TextArea(0,100)]public string[] texttoTest;
    private AudioSource audioClip;
    Coroutine currentDialogue;
    Coroutine blink;
    Animator boxAnimation;

    public Image blinker;
    float displayDuration;
    bool pending;
    float counter;
    

    private void Awake()
    {

        DialogueHandler.Instance.StartDialogue += DisplayDialogue;
        audioClip = GetComponent<AudioSource>();
        passDialogue = InputSystem.actions.FindAction("Accept");
        boxAnimation = GetComponent<Animator>();
      

    }

    private void Start()
    {
      DialogueHandler.Instance.SendDialogue(texttoTest);
    }

    private void Update()
    {
        
        if (pending == true)
        {
            counter += Time.deltaTime;

            if(counter >= 0.5f)
            {
                blinker.enabled = !blinker.enabled;
                counter = 0;
            }
        }
        else
        {
            counter = 0f;
        }
    }

    


    public void DisplayDialogue(string[] dialogue)
    {
        if (currentDialogue != null)
        {
            StopCoroutine(currentDialogue);
        }
        currentDialogue = StartCoroutine(HandleDialogue(dialogue));
      
    }

    IEnumerator HandleDialogue(string[] dialogueList)
    {
        //InitialState
        DialogueHandler.Instance.DialogueStart();
        blinker.enabled = false;
        boxAnimation.Play("OpenBox");
        yield return new WaitUntil(() => boxAnimation.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);
        textBox.text = "";
       
        
        //RollDialogue
        bool format = false;
        foreach (string dialogue in dialogueList)
        {
            blinker.enabled = false;
            textBox.maxVisibleCharacters = 0;
            textBox.text = dialogue;
            bool pass = false;

            //Each char plays a sound
            foreach (char c in dialogue)
            {
                if(passDialogue.IsPressed() == true)
                {
                    pass = true;
                }
                else
                {
                    pass = false;
                }
                if (c == '<')
                {
                    format = true;
                }

                if (format == false)
                {
                    textBox.maxVisibleCharacters++;
                    if(pass == false)
                    {
                        audioClip.Play();
                        yield return new WaitForSeconds(0.045f);
                        audioClip.Stop();
                    }
                    else
                    {
                        yield return new WaitForSeconds(0.01f);
                    }
                    
                }
               
                if(c == '>')
                {
                    format = false;
                }
                
            }

            //Blink
            pending = true;
            yield return new WaitUntil(() => passDialogue.WasPressedThisFrame());
            yield return new WaitUntil(()=> passDialogue.WasReleasedThisFrame());
            pending = false;    
        }
        
        //ResetBoxState
        textBox.text = "";
        boxAnimation.Play("CloseBox");
        blinker.enabled = false;
        yield return new WaitUntil(() => boxAnimation.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);
        DialogueHandler.Instance.DialogueEnd();
        yield return null;
    }

   

   

   
}
