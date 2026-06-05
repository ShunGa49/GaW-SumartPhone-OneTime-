using UnityEngine;

/// <summary>
/// áŠQ•¨
/// </summary>
public class Enemy : MonoBehaviour
{

    [Header("‰æ–ÊŠO‚ÌŠî€")]
    [SerializeField] private float outOfScreen = 7f;

    void Update()
    {
        // ‰º•ûŒü‚ÖˆÚ“®
        this.transform.Translate(Vector3.down * GameManager.Instance.GetSpeed() * Time.deltaTime);

        // ‰æ–ÊŠO‚Öo‚½‚çíœ
        if (this.transform.position.y < -outOfScreen)
        {
            Destroy(this.gameObject);
        }
    }
}