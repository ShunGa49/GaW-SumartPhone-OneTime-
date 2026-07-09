using UnityEngine;

public class HP : MonoBehaviour
{
    [SerializeField] private int maxHP = 3;

    private int currentHP;

    void Start()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;

        Debug.Log(gameObject.name + " HP : " + currentHP);

        if (currentHP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log(gameObject.name + " Dead");

        Destroy(gameObject);
    }
}