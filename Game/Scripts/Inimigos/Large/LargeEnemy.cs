using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeEnemy : MonoBehaviour
{
    [SerializeField] public float speed; // Velocidade do inimigo
    [SerializeField] public float attackRange; // Dist�ncia em que o inimigo come�a a atacar
    [SerializeField] public int attackDamage; // Dano do ataque
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private bool isAttacking; // Flag para verificar se est� atacando
    [SerializeField] private bool isGrounded; // Flag para verificar se o inimigo est� no ch�o

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
    }

    void FixedUpdate()
    {
        // Verifica se o downTarget ainda existe antes de acess�-lo
        if (TargetManager.instance.downTarget != null)
        {
            // Calcular a dire��o em rela��o � torre
            Vector2 direction = new Vector2((TargetManager.instance.downTarget.position.x - transform.position.x), 0).normalized;

            // Verifica a dist�ncia entre o inimigo e a torre (downTarget)
            float distanceToTower = Vector2.Distance(transform.position, TargetManager.instance.downTarget.position);

            // Se estiver dentro do alcance de ataque, para e ataca
            if (distanceToTower <= attackRange && !isAttacking && isGrounded)
            {
                isAttacking = true;
                rb.velocity = Vector2.zero; // Para o inimigo
                Attack();
            }
            else if (distanceToTower > attackRange && isGrounded)
            {
                isAttacking = false; // Reseta o estado de ataque se estiver fora do alcance
                rb.velocity = new Vector2(direction.x * speed, rb.velocity.y); // Apenas movimenta��o horizontal
            }
        }
    }

    void Attack()
    {
        Debug.Log("Inimigo atacou a torre! Dano causado: " + attackDamage);

        if (TargetManager.instance.downTarget != null)
        {
            TowerHP towerHealth = TargetManager.instance.downTarget.GetComponentInParent<TowerHP>();

            if (towerHealth != null)
            {
                towerHealth.TakeDamage(attackDamage);
            }
            else
            {
                Debug.LogWarning("Torre n�o encontrada ou n�o tem o componente TowerHP.");
            }
        }
        else
        {
            Debug.LogWarning("downTarget � null.");
        }

        StartCoroutine(AttackCooldown());
    }

    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(8f); // Tempo de espera entre os ataques
        isAttacking = false; // Permitir que o inimigo ataque novamente
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica se o inimigo colidiu com o ch�o
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Verifica se o inimigo saiu do ch�o
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
