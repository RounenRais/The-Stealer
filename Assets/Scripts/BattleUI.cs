using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleUI : MonoBehaviour
{
    public static BattleUI instance;

    public GameObject mainButtons;
    public GameObject attackButtons;
    public TextMeshProUGUI battleLog;
    // Saldýrý butonlarý — Inspector'dan sürükle
    public Button[] attackButtonList;        // 4 buton
    public TextMeshProUGUI[] attackButtonTexts; // buton yazýlarý
    public void ShowMessage(string message)
    {
        battleLog.gameObject.SetActive(true);
        battleLog.text = message;

    }
    public void HideAllButtons()
{
    mainButtons.SetActive(false);
    attackButtons.SetActive(false);
}
    void Awake()
    {
        instance = this;
    }


    public void OnFightPressed()
    {
        mainButtons.SetActive(false);
        attackButtons.SetActive(true);
    }

    public void OnBackPressed()
    {
        attackButtons.SetActive(false);
        mainButtons.SetActive(true);
    }
    public void HideLog()
    {
        battleLog.gameObject.SetActive(false); 
    }
    void SetupAttackButtons()
    {
        // PlayerBattle'daki unlockedAttacks listesine göre butonlarý ayarla
        for (int i = 0; i < attackButtonList.Length; i++)
        {
            if (i < PlayerBattle.instance.unlockedAttacks.Count)
            {
                // Bu index'te saldýrý var, butonu aktif et
                attackButtonList[i].interactable = true;

                // Buton yazýsýný saldýrý adýyla güncelle
                attackButtonTexts[i].text = PlayerBattle.instance.unlockedAttacks[i].attackName;

                // i'yi kopyala yoksa closure sorunu olur
                int index = i;
                attackButtonList[i].onClick.RemoveAllListeners();
                attackButtonList[i].onClick.AddListener(() =>
                {
                    OnAttackSelected(PlayerBattle.instance.unlockedAttacks[index]);
                });
            }
            else
            {
                // Bu index'te saldýrý yok, butonu kilitle
                attackButtonList[i].interactable = false;
                attackButtonTexts[i].text = "???";
            }
        }
    }

    void OnAttackSelected(AttackData attack)
    {
        attackButtons.SetActive(false);
        mainButtons.SetActive(false);
        BattleManager.instance.PlayerAction(attack);
    }

    // Boss yenince yeni saldýrý eklendiðinde butonlarý güncelle
    public void RefreshAttackButtons()
    {
        SetupAttackButtons();
    }
}