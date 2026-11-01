using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Para ve Can")]
    public int currentCoins = 5; // Başlangıç parası
    [SerializeField] private TMP_Text coinText; // Ekrandaki coin yazısı

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateCoinUI();
    }

    public void AddCoins(int amount)
    {
        currentCoins += amount;
        UpdateCoinUI();
    }

    public void ResetCoins()
    {
        currentCoins = 0;
        UpdateCoinUI();
    }

    public void UpdateCoinUI()
    {
        if (coinText != null)
        {
            coinText.text = currentCoins.ToString();
        }
    }
}