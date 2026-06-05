using UnityEngine;

// Bu satýr Unity'ye "bunu asset olarak oluþturabilirsin" der
// Project panelinde sað týklayýnca menüde çýkar
[CreateAssetMenu(fileName = "NewAttack", menuName = "Battle/AttackData")]

public class AttackData : ScriptableObject
{
    // Saldýrýnýn adý — UI'da butonda görünecek
    public string attackName;

    // Kaç hasar verdiði
    public int damage;

    // Hasar tipi — ileride düþman zayýflýklarýnda kullanacaðýz
    public DamageType damageType;

    // Yan etki — Burn, Stun gibi
    public SideEffect sideEffect;

    // Buton ikonu için
    public Sprite icon;
}

// Hasar tipleri
// Enum'u class dýþýna yazdýk çünkü
// hem Player hem Enemy hem BattleManager kullanacak
public enum DamageType
{
    Physical,
    Fire,
    Ice,
    Poison
}

// Yan etkiler
public enum SideEffect
{
    None,
    Burn,    // her tur hasar
    Stun,    // tur atlýyor
    Bleed    // her tur hasar
}