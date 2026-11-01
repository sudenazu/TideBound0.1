
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Panels")]
    public GameObject panelWin;
    public GameObject panelLose;

    [Header("Timer Settings")]
    public float timeRemaining = 30f;
    private bool isGameOver = false;

    [Header("Stats")]
    public static int currentCoins = 0;
    public static bool sunPanelPurchased = false;

    // Tıklama sayaçları
    private int click3 = 0;
    private int click4 = 0;
    private int click5 = 0;
    private int click7 = 0;
    private int click8 = 0;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if (panelWin) panelWin.SetActive(false);
        if (panelLose) panelLose.SetActive(false);
    }

    void Update()
    {
        if (isGameOver) return;

        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else
        {
            timeRemaining = 0;
            TriggerLose();
        }
    }

    public void CheckPipeRotation(int id)
    {
        if (isGameOver) return;

        Debug.Log("Tıklanan Boru ID'si: " + id);

        if (id == 3) click3 = (click3 + 1) % 4;
        else if (id == 4) click4 = (click4 + 1) % 4;
        else if (id == 5) click5 = (click5 + 1) % 4;
        else if (id == 7) click7 = (click7 + 1) % 4;
        else if (id == 8) click8 = (click8 + 1) % 4;

        CheckWinCondition();
    }

    void CheckWinCondition()
    {
        if (click3 == 1 && click4 == 1 &&
            click5 == 0 && click7 == 0 && click8 == 1)
        {
            TriggerWin();
        }
    }

    void TriggerWin()
    {
        isGameOver = true;

        if (panelWin)
        {
            panelWin.SetActive(true);
            panelWin.transform.SetAsLastSibling();
        }

        Debug.Log("Oyun Kazandı!");
    }

    void TriggerLose()
    {
        isGameOver = true;

        if (panelLose)
        {
            panelLose.SetActive(true);
            panelLose.transform.SetAsLastSibling();
        }

        Debug.Log("Süre Bitti - Kaybettin!");
    }

    // Coin ekleme
    public void AddCoins(int amount)
    {
        currentCoins += amount;
        Debug.Log("Coin: " + currentCoins);
    }

    // Coin sıfırlama
    public void ResetCoins()
    {
        currentCoins = 0;
        Debug.Log("Coin sıfırlandı!");
    }

    public void LoseLife()
    {
        // Mevcut can sistemin buraya eklenebilir.
    }

    public void UpdateCoinUI()
    {
        // Coin arayüzü TaskManager tarafından güncelleniyor.
    }
}
