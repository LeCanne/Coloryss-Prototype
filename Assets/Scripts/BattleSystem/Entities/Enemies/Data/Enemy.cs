using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(RectTransform))]
public class Enemy : Entity, ISelectHandler
{


    private RectTransform m_RectTransform;
    private Image enemySprite;
    public AttackData[] AttackData;
    private Button selectable;
    public event Action<Enemy> patternDone;
    public AudioClip snd_AggroAudio;
    public AudioClip snd_Dead;
   


    private void Awake()
    {
        enemySprite = GetComponent<Image>();
        m_RectTransform = GetComponent<RectTransform>();
        selectable = GetComponent<Button>();

        DisableSelection();
        //SetData(MyEnemyData);
    }

   

    public void InitializeEnemy(EntityData enemyData)
    {
        entityData = enemyData;
        AttackData = enemyData.attacks;
        GraphicEnemyDisplay(enemyData);
        InitializeUnit(enemyData);
    }

    public void DisposeEnemy()
    {
        
        BattleHandler.Instance.KillEnemy(this);
        AudioHandler.Instance.SpawnClip(snd_Dead, 0.4f, transform.position);
        StartCoroutine(Blink(1.2f, 0.1f));
        StartCoroutine(Death(1.2f));
        RemoveActions();
        DisableSelection();
    }

    public void GraphicEnemyDisplay(EntityData enemyData)
    {
        enemySprite.sprite = enemyData.sprite;
        m_RectTransform.sizeDelta = new Vector2(enemyData.sprite.rect.width, enemyData.sprite.rect.height);
    }

    public void EnableSelection()
    {
        selectable.enabled = true;
    }

    public void AddSelectFunction(UnityAction action)
    {
        selectable.onClick.AddListener(action);

    }

    void DisplayName()
    {
        BattleHandler.Instance.SendBattleMessage(unitName);
    }

    public void RemoveActions()
    {
        selectable.onClick.RemoveAllListeners();
    }

    public void DisableSelection()
    {
        selectable.enabled = false;
    }

    private void OnValidate()
    {
        if (TryGetComponent(out Image img))
        {
            enemySprite = img;

        }

        if (TryGetComponent(out RectTransform rt))
        {
            m_RectTransform = rt;
        }

        if (entityData != null)
        {
            maxHp = entityData.maxHp;

            if (enemySprite != null)
                enemySprite.sprite = entityData.sprite;

            if (m_RectTransform != null)
                m_RectTransform.sizeDelta = new Vector2(entityData.sprite.rect.width, entityData.sprite.rect.height);
        }


    }

    public virtual void LaunchAttack()
    {
        AudioHandler.Instance.SpawnClip(snd_AggroAudio, 1.5f, transform.position);
        StartCoroutine(Blink(1.5f, 0.25f));
        StartCoroutine(ProcessAttack(AttackData[0]));
    }

   

    void EndPattern()
    {
        PatternHandler.Instance.patternStopped -= EndPattern;
        patternDone?.Invoke(this);
    }

    public override void RecieveDamage(int damage)
    {
        base.RecieveDamage(damage);
        StartCoroutine(Blink(0.5f, 0.1f));
    }

    public override void HasDied()
    {
        base.HasDied();
        //DisposeEnemy();
    }

    public void OnSelect(BaseEventData eventData)
    {
        DisplayName();
    }

    IEnumerator ProcessAttack(AttackData attackData)
    {
        BattleHandler.Instance.SendBattleMessage(unitName + " " + attackData.Description);
        yield return new WaitForSeconds(2f);
        PatternHandler.Instance.StartPattern(attackData.pattern, this);
        PatternHandler.Instance.patternStopped += EndPattern;
        yield return null;
    }

    IEnumerator Blink(float blinkTime, float interval)
    {
        
        float currentInterval = 0f;
        float currentBlink = 0f;
        while (currentBlink < blinkTime)
        {
            currentBlink += Time.deltaTime;       
            if (currentBlink > currentInterval)
            {
                enemySprite.enabled = !enemySprite.enabled;
                currentInterval += interval;
               
            }
            yield return null;

        }

        enemySprite.enabled = true;
        yield return null;
    }

    IEnumerator Death(float deathTime)
    {
        float timeElapsed = 0;
        while(timeElapsed < deathTime)
        {
            timeElapsed += Time.deltaTime;
            enemySprite.fillAmount = Mathf.Lerp(1, 0, timeElapsed / deathTime);
            yield return null;
        }
        enemySprite.enabled = false;
    }
}

