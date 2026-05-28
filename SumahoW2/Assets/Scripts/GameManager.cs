using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("発射回数")]
    [SerializeField] private int remainingShotCount_max = 1;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI remainText;
    [SerializeField] private GameObject gameClearPanel;
    [SerializeField] private GameObject gameOverPanel;

    private int remainingShotCount_current;
    private int enemyCount = 0;

    private bool canShoot = true;
    // ゲームフラッグ
    private bool isGameClear;
    private bool isGameOver;

    #region Get
    public bool CanShoot => canShoot;
    #endregion

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        remainingShotCount_current = remainingShotCount_max;
        gameClearPanel.SetActive(false);
        gameOverPanel.SetActive(false);

        // UI更新
        remainText.text = "REMAIN : " + remainingShotCount_current--;
    }

    private void Update()
    {
        if (isGameOver)
            return;
    }


    #region 「敵」ゆん
    // +
    public void RegisterEnemy()
    {
        enemyCount++;
    }
    // -
    public void UnregisterEnemy()
    {
        enemyCount--;

        if (enemyCount <= 0)
        {
            enemyCount = 0;

            if (!isGameClear)
            {
                GameClear();
            }
        }
    }
    #endregion

    #region 発射を監視
    /// <summary>
    /// 発射回数加算
    /// </summary>
    public void AddShot()
    {
        if(remainingShotCount_current > 0)
        {
            remainingShotCount_current--;
            canShoot = false;
        }
        // UI更新
        remainText.text = "REMAIN : " + remainingShotCount_current ;
    }

    /// <summary>
    /// 発射できるか？
    /// </summary>
    public void SetCanShoot(bool value)
    {
        canShoot = value;
    }
    #endregion

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

    #region ENDING
    /// <summary>
    /// ゲーム判定
    /// </summary>
    public void CheckGameState()
    {
        if (isGameOver) return;

        // 勝ち
        if (enemyCount <= 0)
        {

            return;
        }

        // 負け条件
        if (remainingShotCount_current <= 0 && enemyCount > 0)
        {
            GameOver();
        }
    }

    /// <summary>
    /// GameClear
    /// </summary>
    public void GameClear()
    {
        if (isGameClear) return;

        isGameClear = true;

        Debug.Log("GAME CLEAR");

        gameClearPanel.SetActive(true);
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
    #endregion
}