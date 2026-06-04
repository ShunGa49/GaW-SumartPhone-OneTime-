using UnityEngine;

/// <summary>
/// 画面下の砲台。フリック方向へミサイル発射
/// </summary>
public class Cannon : MonoBehaviour
{
    [Header("ミサイルPrefab")]
    [SerializeField] private GameObject missilePrefab;

    [Header("画面下からの高さ（0 = 下端 / 1 = 上端）")]
    [Range(0f, 1f)][SerializeField] private float yViewport = 0.1f;


    void OnEnable()
    {
        // フリックイベント登録
        MobileInputVisualizer.OnFlick += Shoot;
    }

    void OnDisable()
    {
        MobileInputVisualizer.OnFlick -= Shoot;
    }

    void Start()
    {
        // 画面下部に
        SetPosition();
    }

    /// <summary>
    /// 画面下中央に配置（初期化時のみ）
    /// </summary>
    /// <summary>
    /// 画面下からの割合で位置決め
    /// </summary>
    void SetPosition()
    {
        Camera cam = Camera.main;
        if (cam == null) return;
        // 画面下部
        Vector3 worldPos = cam.ViewportToWorldPoint(new Vector3(0.5f, yViewport, 10f));
        // 配置
        transform.position = new Vector3(worldPos.x, worldPos.y, 0f);
    }

    /// <summary>
    /// フリック方向へ発射
    /// </summary>
    void Shoot(Vector2 direction)
    {
        GameObject missile = Instantiate(missilePrefab, transform.position, Quaternion.identity);

        missile.GetComponent<Missile>().Init(direction);
    }
}