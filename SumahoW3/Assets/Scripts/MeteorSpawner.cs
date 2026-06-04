using UnityEngine;

/// <summary>
/// 上から隕石をランダム生成
/// </summary>
public class MeteorSpawner : MonoBehaviour
{
    [Header("隕石Prefab")]
    [SerializeField] private GameObject meteorPrefab;

    [Header("生成間隔")]
    [SerializeField] private float interval = 1f;

    [Header("画面上端オフセット")]
    [SerializeField] private float spawnYOffset = 1f;

    [Header("画面下端オフセット（左右範囲調整用）")]
    [SerializeField] private float xMargin = 0.5f;

    void Start()
    {
        // 生成(定期)
        InvokeRepeating(nameof(Spawn), 1f, interval);
    }

    /// <summary>
    /// 隕石生成
    /// </summary>
    void Spawn()
    {
        Camera cam = Camera.main;
        if (cam == null) return;

        // 画面左右端
        Vector3 leftTop = cam.ViewportToWorldPoint(new Vector3(0f, 1f, 10f));

        Vector3 rightTop = cam.ViewportToWorldPoint(new Vector3(1f, 1f, 10f));

        // X範囲（少し内側にする）
        float xMin = leftTop.x + xMargin;
        float xMax = rightTop.x - xMargin;

        float x = Random.Range(xMin, xMax);

        // 上端＋少し上から出す
        float y = leftTop.y + spawnYOffset;

        Vector3 spawnPos = new Vector3(x, y, 0f);

        Instantiate(meteorPrefab, spawnPos, Quaternion.identity);
    }
}