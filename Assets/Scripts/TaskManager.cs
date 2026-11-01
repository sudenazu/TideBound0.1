using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TaskManager : MonoBehaviour
{
    [Header("UI Panelleri ve Metinler")]
    [SerializeField] private GameObject taskPanel;
    [SerializeField] private TMP_Text taskText;
    [SerializeField] private GameObject shopPanel;

    private int currentTaskIndex = 0; // 0: Mini Game, 1: Shop

    void Start()
    {
        UpdateTaskUI();
        if (taskPanel != null) taskPanel.SetActive(false);

        // Oyun başında coin UI'ını güncelle
        if (GameManager.Instance != null)
        {
            GameManager.Instance.UpdateCoinUI();
        }
    }

    public void ToggleTaskPanel()
    {
        if (taskPanel != null)
        {
            taskPanel.SetActive(!taskPanel.activeSelf);
        }
    }

    public void OnClickGoButton()
    {
        if (currentTaskIndex == 0)
        {
            // 1. Görev: MiniGame_1 sahnesine git
            SceneManager.LoadScene("MiniGame_1");
        }
        else if (currentTaskIndex == 1)
        {
            // 2. Görev: Tasks panelini kapat ve Shop panelini aç
            if (taskPanel != null) taskPanel.SetActive(false);
            if (shopPanel != null) shopPanel.SetActive(true);
        }
    }

    void UpdateTaskUI()
    {
        if (taskText != null)
        {
            if (currentTaskIndex == 0)
            {
                taskText.text = "İlk paranı kazan";
            }
            else if (currentTaskIndex == 1)
            {
                taskText.text = "Mağazadan Güneş Paneli al";
            }
        }
    }

    // Mini game kazanıldığında çağrılacak (Örn: Mini game bitiş butonuna bunu bağla)
    public void WinMiniGame()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddCoins(10); // 10 coin ekle
        }

        currentTaskIndex = 1; // 2. göreve geç
        SceneManager.LoadScene("GameScene"); // Ana sahneye dön
    }

    // Mağazadaki Sun_Coin butonuna basıldığında çağrılacak
    public void BuySunCoinItem()
    {
        if (GameManager.Instance != null && GameManager.Instance.currentCoins >= 15)
        {
            GameManager.Instance.ResetCoins(); // Parayı 0 yap
            if (shopPanel != null) shopPanel.SetActive(false); // Shopu kapat
            Debug.Log("Güneş paneli satın alındı, görev tamam!");
        }
        else
        {
            Debug.Log("Yetersiz bakiye!");
        }
    }
}