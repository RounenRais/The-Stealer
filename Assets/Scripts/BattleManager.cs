using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;

    [Header("Referanslar")]
    public EnemyBattle enemyBattle;    // Inspector'dan sürükle
    public PlayerBattle playerBattle;  // Inspector'dan sürükle
    public Image playerImage;
    public enum BattleState { START, PLAYERTURN, ENEMYTURN, WIN, LOSE }
    public BattleState state;

    void Awake()
    {
        instance = this;
    }

    public void StartBattle()
    {
        state = BattleState.START;
        BattleUI.instance.RefreshAttackButtons();
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        BattleUI.instance.HideAllButtons();
        enemyBattle.Setup();

        BattleUI.instance.ShowMessage(enemyBattle.data.enemyName + " appeared !");
        yield return new WaitForSeconds(1.5f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    void PlayerTurn()
    {
        BattleUI.instance.ShowMessage("");
        BattleUI.instance.mainButtons.SetActive(true);
    }

    public void PlayerAction(AttackData attack)
    {
        if (state != BattleState.PLAYERTURN) return;
        StartCoroutine(PlayerAttack(attack));
    }
    IEnumerator FlashRed(Image image)
    {
        // Önce anýnda kýrmýzý yap
        image.color = new Color(1f, 0.2f, 0.2f);

        // Sonra yavaþ yavaþ beyaza dön
        float duration = 0.5f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            image.color = Color.Lerp(new Color(1f, 0.2f, 0.2f), Color.white, elapsed / duration);
            yield return null;
        }

        image.color = Color.white;
    }
    IEnumerator PlayerAttack(AttackData attack)
    {
        BattleUI.instance.HideAllButtons();
        BattleUI.instance.ShowMessage(attack.attackName + " kullandý!");

        yield return new WaitForSeconds(0.8f);

        enemyBattle.TakeDamage(attack.damage);
        StartCoroutine(FlashRed(enemyBattle.EnemyImage)); // hasar anýnda flash

        BattleUI.instance.ShowMessage(
            enemyBattle.data.enemyName + " " + attack.damage + " hasar aldý!"
        );

        yield return new WaitForSeconds(1f);

        if (enemyBattle.IsDead())
        {
            state = BattleState.WIN;
            OnWin();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator EnemyTurn()
    {
        BattleUI.instance.HideAllButtons();

        AttackData enemyAttack = enemyBattle.GetRandomAttack();

        BattleUI.instance.ShowMessage(
            enemyBattle.data.enemyName + " " + enemyAttack.attackName + " kullandý!"
        );
        StartCoroutine(FlashRed(playerImage));
        playerBattle.TakeDamage(enemyAttack.damage);
        yield return new WaitForSeconds(1f);

        BattleUI.instance.ShowMessage(
            enemyAttack.damage + " hasar aldýn!"
        );
        yield return new WaitForSeconds(1.5f);

        if (playerBattle.IsDead())
        {
            state = BattleState.LOSE;
            OnLose();
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    void OnWin()
    {
        BattleUI.instance.ShowMessage("Kazandýn!");

        GameManager.instance.ExitCombat(true);
    }

    void OnLose()
    {
        BattleUI.instance.ShowMessage("Kaybettin!");
        GameManager.instance.GameOver();
    }
}