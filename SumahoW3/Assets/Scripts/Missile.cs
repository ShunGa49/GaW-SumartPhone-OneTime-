using UnityEngine;

/// <summary>
/// フリック方向に飛ぶ弾
/// </summary>
public class Missile : MonoBehaviour
{
    [Header("移動速度")]
    [SerializeField] private float speed = 15f;

    private Vector2 direction;

    /// <summary>
    /// 発射方向を初期化
    /// </summary>
    public void Init(Vector2 dir)
    {
        direction = dir;
    }

    void Update()
    {
        // 直進移動
        transform.Translate(direction * speed * Time.deltaTime);
        // 画面外削除
        if (Mathf.Abs(transform.position.x) > 10 || Mathf.Abs(transform.position.y) > 20)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // 隕石に当たったら破壊
        if (col.CompareTag("Meteor"))
        {
            Destroy(col.gameObject);
            Destroy(gameObject);
            // スコア加算
            GameManager.Instance.AddScore(1);
        }
    }
}