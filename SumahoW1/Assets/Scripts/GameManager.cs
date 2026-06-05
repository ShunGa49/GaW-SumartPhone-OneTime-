using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// ゲーム全体管理
/// </summary>
public class GameManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private GameObject gameOverPanel;

    [Header("Game Speed")]
    [SerializeField] private float baseSpeed = 5f;
    [SerializeField] private float speedIncreaseRate = 0.1f;

    private float currentSpeed;

    // シングル豚
    public static GameManager Instance;

    // ゲームオーバーフラッグ
    private bool isGameOver;
    // スコア
    private float score;



    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        currentSpeed = baseSpeed;
        // 初期状態は非表示
        gameOverPanel.SetActive(false);
    }

    void Update()
    {
        if (isGameOver)
            return;

        // 生存時間加算
        score += Time.deltaTime;
        // 速度上昇
        currentSpeed += speedIncreaseRate * Time.deltaTime;
        // UI更新
        timeText.text = "TIME : " + score.ToString("F2");
    }

    /// <summary>
    /// 速度取得
    /// </summary>
    public float GetSpeed()
    {
        return currentSpeed;
    }

    /// <summary>
    /// GameOver
    /// </summary>
    public void GameOver()
    {
        if (isGameOver)
            return;
        
        isGameOver = true;
        Debug.Log("GAME OVER");

        // UI表示
        gameOverPanel.SetActive(true);
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