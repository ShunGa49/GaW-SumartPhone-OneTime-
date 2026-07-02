using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // UŒ‚—Í
    [SerializeField] private int attackPower = 1;

    // UŒ‚”»’è
    private void OnTriggerEnter(Collider other)
    {
        // Enemyƒ^ƒO‚¾‚¯UŒ‚
        if (other.CompareTag("Enemy"))
        {
            HP hp = other.GetComponent<HP>();

            if (hp != null)
            {
                hp.TakeDamage(attackPower);
            }
        }
    }
}