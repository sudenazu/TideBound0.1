
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TaskManager : MonoBehaviour
{
    [Header("UI Panelleri")]
    [SerializeField] private GameObject taskPanel;
    [SerializeField] private TMP_Text taskText;
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private TMP_Text coinText;

    [Header("Mini Game Panelleri")]
    [SerializeField] private GameObject panelWin;
    [SerializeField] private GameObject panelLose;

    public static int currentTaskIndex = 0;

    void Start()
    {
        UpdateTaskUI();
        UpdateCoinText();

        if (taskPanel != null) taskPanel.SetActive(false);
        if (shopPanel != null) shopPanel.SetActive(false);

        if (panelWin != null) panelWin.SetActive(false);
        if (panelLose != null) panelLose.SetActive(false);
    }

    public void ToggleTaskPanel()
    {
        if (taskPanel != null)
            taskPanel.SetActive(!taskPanel.activeSelf);
    }

    public void OnClickGoButton()
    {
        if (currentTaskIndex == 0)
        {
            SceneManager.LoadScene("MiniGame_1");
        }
        else if (currentTaskIndex == 1)
        {
            if (taskPanel != null) taskPanel.SetActive(false);
            if (shopPanel != null) shopPanel.SetActive(true);
        }
    }

    void UpdateTaskUI()
    {
        if (taskText == null) return;

        if (currentTaskIndex == 0)
            taskText.text = "İlk paranı kazan";

        else if (currentTaskIndex == 1)
            taskText.text = "Mağazadan Güneş Paneli al";

        else
            taskText.text = "Güneş paneli satın alındı!";
    }

    public void ShowWinPanel()
    {
        if (panelWin != null)
            panelWin.SetActive(true);
    }

    public void ShowLosePanel()
    {
        if (panelLose != null)
            panelLose.SetActive(true);
    }

    // Mini game kazan butonu
    public void OnClickWinButton()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddCoins(10);
        }

        currentTaskIndex = 1;

        SceneManager.LoadScene("GameScene");
    }

    // Tekrar dene butonu
    public void OnClickTryAgainButton()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.LoseLife();
        }

        SceneManager.LoadScene("GameScene");
    }

    // Güneş paneli satın alma

    public void BuySunCoinItem()
    {
        if (GameManager.sunPanelPurchased)
        {
            Debug.Log("Güneş paneli zaten satın alındı!");
            return;
        }

        if (GameManager.currentCoins >= 10)
        {
            GameManager.Instance.ResetCoins();

            GameManager.sunPanelPurchased = true;

            currentTaskIndex = 2;

            // Evleri güncelle
            HouseManager houseManager = FindFirstObjectByType<HouseManager>();

            if (houseManager != null)
            {
                houseManager.UpdateHouse();
            }
            else
            {
                Debug.LogError("HouseManager bulunamadı!");
            }

            if (shopPanel != null)
                shopPanel.SetActive(false);

            UpdateCoinText();
            UpdateTaskUI();

            Debug.Log("Güneş paneli satın alındı!");
        }
        else
        {
            Debug.Log("Yeterli coin yok!");
        }
    }

    void UpdateCoinText()
    {
        if (coinText != null)
        {
            coinText.text = GameManager.currentCoins.ToString();
        }
    }
}
