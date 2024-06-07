using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    float speed = 0;
    Vector3 playerPosition;
    [SerializeField] float runThreshold = 10f;
    [SerializeField] float walkThreshold = 50f;
    [SerializeField] RuntimeAnimatorController animatorController;
    Animator animator;
    bool isDead = false;
    bool isAttacking;
    GameObject player;
    int currentHealth;

    private void Start()
    {
        player = GameObject.Find("Soldier");

        animator = this.GetComponent<Animator>();
        if (animator != null)
        {
            animator.runtimeAnimatorController = animatorController; // Atribuir o AnimatorController
        }

        currentHealth = 100;
    }

    private void Update()
    {
        if (isDead)
            return;

        // Atualizar a posição do jogador continuamente
        playerPosition = player.transform.position;
        playerPosition.y = 1;

        float distanceToPlayer = Vector3.Distance(transform.position, playerPosition);

        if (distanceToPlayer <= runThreshold)
        {
            AttackPlayer(distanceToPlayer);
        }
        else if (distanceToPlayer <= walkThreshold)
        {
            WalkInCircles();
        }
        else
        {
            // Parar o zumbi se estiver fora do alcance de andar e correr
            animator.SetBool("isRunning", false);
            animator.SetBool("isWalking", false);
        }
    }

    void AttackPlayer(float distanceToPlayer)
    {
        if (distanceToPlayer > 2f) // Parar antes de entrar no jogador
        {
            transform.LookAt(player.transform.position);
            transform.Translate(Vector3.forward * 5f * Time.deltaTime);
            animator.SetBool("isRunning", true);
            animator.SetBool("isWalking", false);
        }
        else
        {
            animator.SetBool("isRunning", false);
            animator.SetBool("isWalking", false);
            if (!isAttacking)
            {
                StartCoroutine(PerformAttack());
            }
        }
    }

    void WalkInCircles()
    {
        animator.SetBool("isWalking", true);
        animator.SetBool("isRunning", false);

        Vector3 circleCenter = player.transform.position;

        float angle = Random.Range(0f, 360f);
        Vector3 circlePosition = circleCenter + Quaternion.Euler(0, angle, 0) * Vector3.forward * 5f;

        transform.LookAt(circleCenter);
        transform.position = Vector3.MoveTowards(transform.position, circlePosition, 1.5f * Time.deltaTime);
    }
    IEnumerator PerformAttack()
    {
        isAttacking = true;
        animator.SetTrigger("Attacking");
        yield return new WaitForSeconds(2f); // Tempo entre os ataques
        isAttacking = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            TakeDamage();
        }
    }
    void Die()
    {
        isDead = true;
        animator.SetTrigger("died");
        Destroy(gameObject, 0.1f);
        GameSceneUIController.Instance.ScoreCount++;
    }

    void TakeDamage()
    {
        currentHealth -= 10;
        if (currentHealth == 0)
        {
            Die();
        }
    }
}
