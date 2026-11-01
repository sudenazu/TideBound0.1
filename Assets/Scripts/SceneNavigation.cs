using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Sahne geçişleri ve panel aç/kapat işlemleri.
/// Bu scripti GameScene'deki bir objeye (örn. "UIManager") ekle,
/// butonların OnClick olaylarından ilgili fonksiyonu seç.
/// </summary>
public class SceneNavigation : MonoBehaviour
{
    [Header("Sahne Adları (Build Settings'e ekli olmalı)")]
    public string gameSceneName = "GameScene";
    public string miniGameSceneName = "MiniGame_1";

    [Header("Paneller (GameScene içindeki UI panelleri)")]
    public GameObject panelShop;
    public GameObject panelTasks;
    public GameObject panelSettings;

    // ---------- SAHNE GEÇİŞLERİ ----------

    // Mini Game butonu -> miniGAme_1 sahnesi
    public void OpenMiniGame()
    {
        SceneManager.LoadScene(miniGameSceneName);
    }

    // Mini game sahnesindeki Çarpı (X) butonu -> GameScene
    public void BackToGameScene()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    // ---------- PANELLER ----------

    public void OpenShop() { CloseAllPanels(); if (panelShop) panelShop.SetActive(true); }
    public void OpenTasks() { CloseAllPanels(); if (panelTasks) panelTasks.SetActive(true); }
    public void OpenSettings() { CloseAllPanels(); if (panelSettings) panelSettings.SetActive(true); }

    // Panellerdeki Çarpı (X) / Kapat butonları -> paneli kapatıp GameScene'e döner
    public void ClosePanelAndReturn()
    {
        CloseAllPanels();

        // Eğer şu an GameScene'de değilsek sahneyi yükle
        if (SceneManager.GetActiveScene().name != gameSceneName)
            SceneManager.LoadScene(gameSceneName);
    }

    void CloseAllPanels()
    {
        if (panelShop) panelShop.SetActive(false);
        if (panelTasks) panelTasks.SetActive(false);
        if (panelSettings) panelSettings.SetActive(false);
    }
}