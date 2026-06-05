using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBattle : MonoBehaviour
{
    public static EnemyBattle instance;

    public Image EnemyImage;
    public EnemyData data;
    public int currentHP;
    public Image hpBar;
    public bool isDefeated = false;

    int spriteCounter = 0;
    float animationTimer = 0f;
    public float animationSpeed = 0.12f; // sprite deðiþme süresi

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        AnimateEnemy();
    }

    void AnimateEnemy()
    {
        if (data == null) return;
        if (data.animationSprites == null) return;
        if (data.animationSprites.Count == 0) return;
        if (EnemyImage == null) return;

        animationTimer += Time.deltaTime;

        if (animationTimer >= animationSpeed)
        {
            animationTimer = 0f;

            EnemyImage.sprite = data.animationSprites[spriteCounter];
            spriteCounter = (spriteCounter + 1) % data.animationSprites.Count;
        }
    }

    public void Setup()
    {
        currentHP = data.maxHP;
        spriteCounter = 0;
        animationTimer = 0f;

        UpdateHPBar();

        if (data.animationSprites != null && data.animationSprites.Count > 0)
        {
            EnemyImage.sprite = data.animationSprites[0];
        }
        else
        {
            LoadEnemySprite();
        }
    }

    public void LoadEnemySprite()
    {
        if (EnemyImage != null && data.battleSprite != null)
        {
            EnemyImage.sprite = data.battleSprite;
        }
    }

    public void TakeDamage(int amount)
    {
        currentHP -= amount;
        currentHP = Mathf.Clamp(currentHP, 0, data.maxHP);

        Debug.Log($"{data.enemyName} took {amount} damage! Current HP: {currentHP}");

        UpdateHPBar();
    }

    void UpdateHPBar()
    {
        // Lerp ile animasyonlu geçiþ
        StartCoroutine(AnimateHPBar());
    }

    IEnumerator AnimateHPBar()
    {
        float targetFill = (float)currentHP / data.maxHP;
        float currentFill = hpBar.fillAmount;
        float elapsed = 0f;

        while (elapsed < 0.5f)
        {
            elapsed += Time.deltaTime;
            hpBar.fillAmount = Mathf.Lerp(currentFill, targetFill, elapsed / 0.5f);
            yield return null;
        }

        hpBar.fillAmount = targetFill;
    }
    public bool IsDead()
    {
        return currentHP <= 0;
    }

    public AttackData GetRandomAttack()
    {
        int index = Random.Range(0, data.attackList.Count);
        return data.attackList[index];
    }
}