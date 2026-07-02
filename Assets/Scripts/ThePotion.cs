using UnityEngine;

public class ThePotion : MonoBehaviour
{
    // Anahtarýn item verisi
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Potion collected, healing player by 30 HP.");
            if (PlayerBattle.instance.currentHP+30>150)
            {
                PlayerBattle.instance.Heal(150-PlayerBattle.instance.currentHP);
            }
            else
            {
                PlayerBattle.instance.Heal(30);

            }

            Destroy(gameObject);
        }
    }
}
