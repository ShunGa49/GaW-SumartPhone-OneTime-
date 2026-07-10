using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour
{
    [SerializeField] private int maxHP = 3;
    [SerializeField] private Image hpBar;

    private int currentHP;

    void Start()
    {
        currentHP = maxHP;
        UpdateHPBar();
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;

        if (currentHP < 0)
            currentHP = 0;

        UpdateHPBar();

        Debug.Log(gameObject.name + " HP : " + currentHP);

        if (currentHP <= 0)
        {
            Die();
        }
    }

    void UpdateHPBar()
    {
        if (hpBar != null)
        {
            hpBar.fillAmount = (float)currentHP / maxHP;
        }
    }

    void Die()
    {
        if (CompareTag("Enemy"))
        {
            Debug.Log("PLAYER WIN");
        }

        if (CompareTag("Player"))
        {
            Debug.Log("GAME OVER");
        }

        Destroy(gameObject);
    }
}