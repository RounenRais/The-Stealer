using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBattle : MonoBehaviour
{
    public static PlayerBattle instance;
    public int maxHP = 100;
    public int currentHP;
    // Boss kestikçe buraya eklenecek
    public List<AttackData> unlockedAttacks = new List<AttackData>();
    // Inspector'dan HP barýný baðla
    public Image hpBar;

    void Awake()
    {
        instance = this;
        currentHP = maxHP;
    }
 
     public void AddMove(AttackData attack)
    {
        if (unlockedAttacks.Contains(attack))
        {
            return;
        }
        if (unlockedAttacks.Count>=4) {
            unlockedAttacks.RemoveAt(0);
        }
        unlockedAttacks.Add(attack);
        BattleUI.instance.RefreshAttackButtons();
    }
    public void TakeDamage(int amount)
    {
        currentHP -= amount;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP); 
        UpdateHPBar();
    }
    public void Heal(int amount)
    {
        currentHP += amount;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP); 
        UpdateHPBar();
    }
 

    void UpdateHPBar()
    {
        // Lerp ile animasyonlu geçiþ
        StartCoroutine(AnimateHPBar());
    }
    IEnumerator AnimateHPBar()
    {
        float targetFill = (float)currentHP / maxHP;
        float currentFill = hpBar.fillAmount;
        float elapsed = 0f;
        while (elapsed < 0.5f)
        {
            elapsed += Time.deltaTime;
            hpBar.fillAmount = Mathf.Lerp(currentFill, targetFill, elapsed / 0.5f);
            yield return null;
        }
        GameManager.instance.UpdateHpUI();
        hpBar.fillAmount = targetFill;
    }
    public bool IsDead()
    {
        return currentHP <= 0;
    }
}