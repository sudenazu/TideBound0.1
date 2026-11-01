using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PipeGameManager : MonoBehaviour
{
    [Header("Grid (3x3, soldan sağa, yukarıdan aşağıya 9 tile)")]
    public PipeTile[] tiles = new PipeTile[9];
    public int gridSize = 3;

    [Header("Güç girişi / çıkışı")]
    public int startRow = 1;   // Power sol kenardan bu satıra bağlı
    public int endRow = 1;     // Hedef sağ kenardan bu satırda

    [Header("Oyun")]
    public float timeLimit = 30f;
    public int coinReward = 10;

    [Header("UI")]
    public TMP_Text timeText;
    public TMP_Text scoreText;
    public TMP_Text coinText;
    public GameObject panelWin;
    public GameObject panelLose;
    public Button replayButtonWin;
    public Button replayButtonLose;

    public bool IsPlaying { get; private set; }

    float timeLeft;
    int coins;

    const string CoinKey = "coins";

    void Start()
    {
        foreach (var t in tiles) t.manager = this;

        replayButtonWin.onClick.AddListener(StartGame);
        replayButtonLose.onClick.AddListener(StartGame);

        coins = PlayerPrefs.GetInt(CoinKey, 0);
        StartGame();
    }

    public void StartGame()
    {
        panelWin.SetActive(false);
        panelLose.SetActive(false);

        // Rastgele döndür; ilk anda çözülmüş olmasın
        do
        {
            foreach (var t in tiles) t.SetRotation(Random.Range(0, 4));
        } while (IsSolved());

        timeLeft = timeLimit;
        IsPlaying = true;
        UpdateUI();
    }

    void Update()
    {
        if (!IsPlaying) return;

        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0f)
        {
            timeLeft = 0f;
            Lose();
        }
        UpdateUI();
    }

    public void CheckWin()
    {
        if (IsSolved()) Win();
    }

    void Win()
    {
        IsPlaying = false;
        coins += coinReward;
        PlayerPrefs.SetInt(CoinKey, coins);
        PlayerPrefs.Save();
        UpdateUI();
        panelWin.SetActive(true);
        panelWin.transform.SetAsLastSibling();   // en öne getir
    }

    void Lose()
    {
        IsPlaying = false;
        panelLose.SetActive(true);
        panelLose.transform.SetAsLastSibling();  // en öne getir
    }

    void UpdateUI()
    {
        if (timeText) timeText.text = "Time: " + Mathf.CeilToInt(timeLeft);
        if (coinText) coinText.text = coins.ToString();
        if (scoreText) scoreText.text = "Score: " + coins;
    }

    // ---------- Bağlantı kontrolü ----------
    // Yönler: 0 Yukarı, 1 Sağ, 2 Aşağı, 3 Sol
    static readonly int[] dRow = { -1, 0, 1, 0 };
    static readonly int[] dCol = { 0, 1, 0, -1 };

    PipeTile Get(int r, int c) => tiles[r * gridSize + c];

    bool IsSolved()
    {
        var visited = new bool[gridSize, gridSize];
        // Power soldan (Sol = 3) ilk tile'a giriyor
        return Walk(startRow, 0, 3, visited);
    }

    bool Walk(int r, int c, int enterSide, bool[,] visited)
    {
        if (r < 0 || c < 0 || r >= gridSize || c >= gridSize) return false;
        if (visited[r, c]) return false;

        var tile = Get(r, c);
        if (!tile.IsOpen(enterSide)) return false;   // giriş kenarı açık değil
        visited[r, c] = true;

        for (int side = 0; side < 4; side++)
        {
            if (side == enterSide || !tile.IsOpen(side)) continue;

            // Sağ kenardan çıkış: hedef satırdaysa kazandık
            if (side == 1 && c == gridSize - 1)
            {
                if (r == endRow) return true;
                continue;
            }

            int nr = r + dRow[side];
            int nc = c + dCol[side];
            if (Walk(nr, nc, (side + 2) % 4, visited)) return true;
        }
        return false;
    }
}