using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum EnemyState
    {
        Idle,
        Attack
    }

    [SerializeField] private Animator animator;
    [SerializeField] private Transform player;

    [SerializeField] private float attackDistance = 2f;
    [SerializeField] private float attackInterval = 1.5f;

    private EnemyState state = EnemyState.Idle;
    private float timer;

    void Update()
    {
        if (player == null)
            return;

        float distance = Vector3.Distance(transform.position, player.position);

        switch (state)
        {
            case EnemyState.Idle:

                timer += Time.deltaTime;

                if (distance <= attackDistance && timer >= attackInterval)
                {
                    timer = 0;
                    state = EnemyState.Attack;
                    animator.SetTrigger("Attack");
                }

                break;

            case EnemyState.Attack:
                break;
        }
    }

    // Attackアニメーションの最後にAnimationEventで呼ぶ
    public void EndAttack()
    {
        state = EnemyState.Idle;
    }
}