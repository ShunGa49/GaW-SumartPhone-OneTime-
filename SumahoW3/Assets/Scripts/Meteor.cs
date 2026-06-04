using UnityEngine;

/// <summary>
/// 上から落ちてくる隕石
/// </summary>
public class Meteor : MonoBehaviour
{
    [Header("落下速度")]
    [SerializeField] private float fallSpeed = 2f;

    void Update()
    {
        // 下へ。～White Illumination～
        transform.Translate(Vector2.down * fallSpeed * Time.deltaTime);

        // 地面に到達したらゲームオーバー
        if (transform.position.y < -6f)
        {
            GameManager.Instance.GameOver();
            Destroy(gameObject);
        }
    }
}