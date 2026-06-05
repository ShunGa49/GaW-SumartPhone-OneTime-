using UnityEngine;

/// <summary>
/// 障害物生成クラス
/// </summary>
public class Spawner : MonoBehaviour
{
    [Header("障害物Prefab")]
    [SerializeField] private GameObject enemyPrefab;

    [Header("生成間隔")]
    [SerializeField] private float interval = 1f;

    [Header("各レーンX座標")]
    [SerializeField]
    private float[] laneX =
    {
        -2f,
        0f,
        2f
    };

    [Header("生成Y座標")]
    [SerializeField] private float spawnY = 6f;

    /// <summary>
    /// ゲーム開始時
    /// </summary>
    void Start()
    {
        // 一定間隔でSpawn実行
        InvokeRepeating(nameof(Spawn), 1f, interval);
    }

    /// <summary>
    /// 障害物生成
    /// </summary>
    void Spawn()
    {
        // ランダムレーン選択
        int lane = Random.Range(0, laneX.Length);

        // 生成位置
        Vector3 pos = new Vector3(laneX[lane], spawnY, 0f);

        // 生成
        Instantiate(enemyPrefab, pos, Quaternion.identity);
    }
}