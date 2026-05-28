using UnityEngine;

public class BallShot : MonoBehaviour
{
    [Header("つよさ")]
    [SerializeField] private float power = 0.05f;

    [Header("入力")]
    [SerializeField] private MobileInputVisualizer inputVisualizer;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // スワイプイベント登録
        inputVisualizer.OnSwipe += Shot;
    }

    private void OnDestroy()
    {
        // イベント解除
        inputVisualizer.OnSwipe -= Shot;
    }

    // スワイプ時のショット処理
    void Shot(Vector2 swipe)
    {
        if (!GameManager.Instance.CanShoot) return;

        // 方向
        Vector2 direction = -swipe.normalized;

        // スワイプ距離で強さ変化
        float force = swipe.magnitude * power;

        rb.linearVelocity = Vector2.zero;

        // 加速ッ
        rb.AddForce(direction * force, ForceMode2D.Impulse);

        GameManager.Instance.AddShot();

        GameManager.Instance.SetCanShoot(false);
    }

    private void FixedUpdate()
    {
        // 完全停止チェック
        if (GameManager.Instance.CanShoot) return;

        if (rb.linearVelocity.magnitude < 0.05f)
        {
            rb.linearVelocity = Vector2.zero;
            GameManager.Instance.SetCanShoot(true);
        }

        // クリア判定
        GameManager.Instance.CheckGameState();
    }
}