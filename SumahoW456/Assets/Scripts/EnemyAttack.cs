using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private int attackPower = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HP hp = other.GetComponent<HP>();

            if (hp != null)
            {
                hp.TakeDamage(attackPower);
            }
        }
    }
}