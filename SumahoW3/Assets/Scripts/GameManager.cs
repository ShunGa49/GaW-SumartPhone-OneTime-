using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// スコア管理・ゲームオーバー管理
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI")]
    [SerializeField] private GameObject gameOverPanel; // GameOverパネル
    [SerializeField] private TextMeshProUGUI timeText; // 生存時間表示
    [SerializeField] private TextMeshProUGUI scoreText; // スコア表示

    private int score;           // スコア
    private bool isGameOver;     // ゲームオーバー判定
    private float surviveTime;   // 生存時間

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        gameOverPanel.SetActive(false);
    }

    void Update()
    {
        // 生存時間カウント（ゲーム中のみ）
        if (isGameOver) return;

        surviveTime += Time.deltaTime;

        UpdateUI();
    }

    /// <summary>
    /// スコア加算
    /// </summary>
    public void AddScore(int value)
    {
        if (isGameOver) return;

        score += value;
        Debug.Log("Score: " + score);
    }

    /// <summary>
    /// ゲームオーバー処理
    /// </summary>
    public void GameOver()
    {
        if (isGameOver) return;

        isGameOver = true;

        // パネル表示
        gameOverPanel.SetActive(true);

        // 最終UI更新
        UpdateUI();

        // 時間停止
        Time.timeScale = 0f;
    }

    /// <summary>
    /// UI更新
    /// </summary>
    void UpdateUI()
    {
        // スコア
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
        // 生きた時間
        if (timeText != null)
        {
            timeText.text = "Time: " + surviveTime.ToString("F1");
        }
    }

    #region ボタン呼出
    /// <summary>
    /// リトライ
    /// </summary>
    public void OnRetry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// タイトル
    /// </summary>
    public void OnTitle()
    {
        SceneManager.LoadScene("Title");
    }
    #endregion
}