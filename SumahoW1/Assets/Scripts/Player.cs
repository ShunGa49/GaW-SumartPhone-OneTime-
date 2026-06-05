using System.Collections;
using UnityEngine;

/// <summary>
/// プレイヤーのレーン移動を管理するクラス
/// </summary>
public class Player : MonoBehaviour
{
    // 各レーンのX座標
    [SerializeField]
    private float[] laneX =
    {
        -2f,
        0f,
        2f
    };

    [Header("移動速度")]
    [SerializeField] private float moveSpeed = 10f;

    // 今いるレーン番号
    private int currentLane = 1;
    // 現在動いているCoroutine保存
    private Coroutine moveCoroutine;

    void Start()
    {
        // 最初は中央に配置
        MoveImmediate();
    }

    /// <summary>
    /// 左移動
    /// </summary>
    public void MoveLeft()
    {
        // すでに左端なら何もしない
        if (currentLane <= 0)
            return;
        // レーン番号を1減らす
        currentLane--;
        // 移動開始
        MoveToLane();
    }

    /// <summary>
    /// 右移動
    /// </summary>
    public void MoveRight()
    {
        // すでに右端なら何もしない
        if (currentLane >= laneX.Length - 1)
            return;
        // レーン番号を1増やす
        currentLane++;
        // 移動開始
        MoveToLane();
    }

    /// <summary>
    /// 即座に現在レーンへ移動
    /// </summary>
    void MoveImmediate()
    {
        Vector3 pos = this.transform.position;
        // 現在レーンのX座標へ
        pos.x = laneX[currentLane];
        this.transform.position = pos;
    }

    /// <summary>
    /// なめらか移動開始
    /// </summary>
    void MoveToLane()
    {
        // 目標位置
        Vector3 target = this.transform.position;
        // 目標Xを現在レーンに設定
        target.x = laneX[currentLane];

        // すでに移動中なら停止
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }
        // Coroutine開始
        moveCoroutine = StartCoroutine(MoveRoutine(target));
    }

    /// <summary>
    /// なめらか移動Coroutine
    /// </summary>
    IEnumerator MoveRoutine(Vector3 target)
    {
        // 目標位置に近づくまで繰り返す
        while (Vector3.Distance(this.transform.position, target) > 0.01f) 
        {
            // 徐々に移動
            this.transform.position = Vector3.MoveTowards(this.transform.position, target, moveSpeed * Time.deltaTime);
            // 1フレーム待機
            yield return null;
        }
        // 最後に完全一致させる
        this.transform.position = target;
    }

    /// <summary>
    /// 障害物と衝突したとき
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // GameOver実行
        GameManager.Instance.GameOver();
    }
}