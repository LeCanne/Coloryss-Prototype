using System;
using System.Collections;
using UnityEditor.U2D.Sprites;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent (typeof(Image))]
[RequireComponent(typeof(RectTransform))]
public class Enemy : Entity
{


    private RectTransform m_RectTransform;
    private Image enemySprite;
    public PatternData[] patterns;
    private Button selectable;
    public event Action<Enemy> patternDone;


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
        patterns = enemyData.patterns;
        GraphicEnemyDisplay(enemyData);
        InitializeUnit(enemyData);
    }

    public void DisposeEnemy()
    {
        enemySprite.enabled = false;
        BattleHandler.Instance.KillEnemy(this);
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

    public void AddFunction(UnityAction action)
    {
       selectable.onClick.AddListener(action);
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
        if(TryGetComponent(out Image img))
        {
            enemySprite = img;
            
        }

        if(TryGetComponent(out RectTransform rt))
        {
            m_RectTransform = rt;
        }
       
        if(entityData != null)
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
       ProcessAttack(patterns[0]);
    }

    void ProcessAttack(PatternData pattern)
    {
        PatternHandler.Instance.StartPattern(pattern, this);
        PatternHandler.Instance.patternStopped += EndPattern;
    }
   
    void EndPattern()
    {
        PatternHandler.Instance.patternStopped -= EndPattern;
        patternDone?.Invoke(this);
    }

    public override void HasDied()
    {
         base.HasDied();
         DisposeEnemy();
    }

    
}
