using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private int attackPower = 1;

    private void OnTriggerEnter(Collider other)
    {
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