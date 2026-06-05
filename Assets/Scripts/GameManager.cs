using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject battleCanvas;
    public PlayerMovement playerMovement;
    public GameObject worldObjects;
    public GameObject gameOverCanvas;
    private WorldEnemy currentEnemy;
    public GameObject inventoryCanvas;
    public Image BottomHp;
    public TextMeshProUGUI HpText;
    void Awake()
    {
        if (instance == null) { instance = this;  }
        else Destroy(gameObject);
    }

    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }
    public void EnterCombat(WorldEnemy enemy)
    {
        currentEnemy = enemy;
        worldObjects.SetActive(false);
        battleCanvas.SetActive(true);
        playerMovement.enabled = false;
        EnemyBattle.instance.data = enemy.data;
        BattleManager.instance.StartBattle();


    }

    public void ExitCombat(bool playerWon)
    {
        worldObjects.SetActive(true);
        battleCanvas.SetActive(false);
        playerMovement.enabled = true;
        float targetFill = (float) PlayerBattle.instance.currentHP/ PlayerBattle.instance.maxHP;
        BottomHp.fillAmount = targetFill;
        HpText.text = PlayerBattle.instance.currentHP.ToString() + " / " + PlayerBattle.instance.maxHP.ToString();
        if (playerWon && currentEnemy != null)
        {
            currentEnemy.isDefeated = true;
            currentEnemy.gameObject.SetActive(false);
            currentEnemy = null;
        }

    }

    public void GameOver()
    {
        worldObjects.SetActive(false);
        battleCanvas.SetActive(false);
        gameOverCanvas.SetActive(true);
        playerMovement.enabled = false;
    }

    public void TryAgain()
    {
        currentEnemy = null;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }


}