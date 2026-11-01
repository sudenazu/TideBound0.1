using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Paneller")]
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject mainButtonsPanel;

    [Header("Ayarlar")]
    [SerializeField] private string firstLevelName = "GameScene";

    // Start Butonu
    public void OnClickStart()
    {
        // Hedef sahneyi yükler
        SceneManager.LoadScene(firstLevelName);
    }

    // Settings Butonu
    public void OnClickOpenSettings()
    {
        if (mainButtonsPanel != null) mainButtonsPanel.SetActive(false);
        if (settingsPanel != null) settingsPanel.SetActive(true);
    }

    // Settings İçi "Geri" Butonu
    public void OnClickCloseSettings()
    {
        if (settingsPanel != null) settingsPanel.SetActive(false);
        if (mainButtonsPanel != null) mainButtonsPanel.SetActive(true);
    }

    // Quit Butonu
    public void OnClickQuit()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}