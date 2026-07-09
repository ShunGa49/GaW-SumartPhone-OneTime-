using UnityEngine;

public class EnemyAttackController : MonoBehaviour
{
    [SerializeField] private Collider attackCollider;

    void Start()
    {
        attackCollider.enabled = false;
    }

    public void EnableAttack()
    {
        attackCollider.enabled = true;
    }

    public void DisableAttack()
    {
        attackCollider.enabled = false;
    }
}