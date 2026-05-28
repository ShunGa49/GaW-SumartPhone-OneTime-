using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("体力")]
    [SerializeField] private int hpMax = 3;

    [Header("HPバー")]
    [SerializeField] private Image hpBar;

    private int hpCurrent;

    private void Start()
    {
        GameManager.Instance.RegisterEnemy();

        hpCurrent = hpMax;
        UpdateHPBar();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            hpCurrent--;

            UpdateHPBar();

            if (hpCurrent <= 0)
            {
                GameManager.Instance.UnregisterEnemy();
                Destroy(this.gameObject);
            }
        }
    }

    /// <summary>
    /// HPバー
    /// </summary>
    private void UpdateHPBar()
    {
        hpBar.fillAmount = (float)hpCurrent / hpMax;
    }
}