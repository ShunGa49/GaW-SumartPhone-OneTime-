using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [SerializeField] private Transform player;

    [SerializeField] private float attackDistance = 2f;

    [SerializeField] private float attackInterval = 1.5f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (player == null)
            return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= attackDistance)
        {
            if (timer >= attackInterval)
            {
                timer = 0;

                animator.SetTrigger("Attack");
            }
        }
    }
}