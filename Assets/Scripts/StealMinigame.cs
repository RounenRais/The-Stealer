using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StealMinigame : MonoBehaviour
{
    public static StealMinigame instance;

    [Header("UI")]
    public GameObject stealPanel;
    public RectTransform indicator;
    public RectTransform hitZone;
    public RectTransform timingBar;

    [Header("Aþama Ayarlarý")]
    float[] speeds = { 200f, 350f, 500f };
    float[] hitZoneSizes = { 120f, 70f, 35f };

    int currentStage = 0;
    float direction = 1f;
    bool isRunning = false;
    bool waitingForInput = false;

    float barWidth;

    void Awake()
    {
        instance = this;
        stealPanel.SetActive(false);
    }

    public void StartMinigame()
    {
        currentStage = 0;
        stealPanel.SetActive(true);
        barWidth = timingBar.rect.width;
        StartCoroutine(RunStage());
    }

    IEnumerator RunStage()
    {
        // HitZone boyutunu ayarla
        hitZone.sizeDelta = new Vector2(hitZoneSizes[currentStage], hitZone.sizeDelta.y);

        // Indicator'ý sola sýfýrla
        indicator.anchoredPosition = new Vector2(-barWidth / 2f, -125.4f);
        direction = 1f;
        isRunning = true;
        waitingForInput = true;
        yield return null;
    }

    void Update()
    {
        if (!isRunning || !waitingForInput) return;

        // Indicator'ý hareket ettir
        float speed = speeds[currentStage];
        Vector2 pos = indicator.anchoredPosition;
        pos.x += direction * speed * Time.deltaTime;

        // Kenara gelince geri dönsün
        if (pos.x >= barWidth / 2f)
        {
            pos.x = barWidth / 2f;
            direction = -1f;
        }
        else if (pos.x <= -barWidth / 2f)
        {
            pos.x = -barWidth / 2f;
            direction = 1f;
        }

        indicator.anchoredPosition = pos;

        // Týklama kontrolü
        if (Input.GetMouseButtonDown(0))
        {
            CheckHit();
        }
    }

    void CheckHit()
    {
        waitingForInput = false;
        isRunning = false;

        float indicatorX = indicator.anchoredPosition.x;
        float hitZoneHalf = hitZoneSizes[currentStage] / 2f;

        if (Mathf.Abs(indicatorX) <= hitZoneHalf)
        {
            // Baþarýlý
            currentStage++;

            if (currentStage >= 3)
            {
                // 3 aþama tamam, steal baþarýlý
                StartCoroutine(StealSuccess());
            }
            else
            {
                // Sonraki aþama
                StartCoroutine(NextStage());
            }
        }
        else
        {
            // Kaçýrdý
            StartCoroutine(StealFail());
        }
    }

    IEnumerator NextStage()
    {
        yield return new WaitForSeconds(0.3f);
        StartCoroutine(RunStage());
    }

    IEnumerator StealSuccess()
    {
        stealPanel.SetActive(false);
        BattleManager.instance.OnStealSuccess();
        yield return null;
    }

    IEnumerator StealFail()
    {
        stealPanel.SetActive(false);
        BattleManager.instance.OnStealFail();
        yield return null;
    }
}