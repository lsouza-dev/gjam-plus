using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class FlyEnemys : MonoBehaviour
{
    [SerializeField] public float speed; // Velocidade do inimigo
    [SerializeField] public float attackRange; // Dist�ncia em que o inimigo come�a a atacar
    [SerializeField] public int attackDamage; // Dano do ataque
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private bool isAttacking; // Flag para verificar se est� atacando

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        // Verifica se o downTarget ainda existe antes de acess�-lo
        if (TargetManager.instance.downTarget != null)
        {
            // Calcula a dire��o em rela��o ao alvo (torre)
            Vector2 direction = (TargetManager.instance.highTarget.position - transform.position).normalized;

            // Verifica a dist�ncia entre o inimigo e a torre (highTarget)
            float distanceToTower = Vector2.Distance(transform.position, TargetManager.instance.highTarget.position);

            // Se estiver dentro do alcance de ataque, para e ataca
            if (distanceToTower <= attackRange && !isAttacking)
            {
                isAttacking = true;
                rb.velocity = Vector2.zero; // Para o inimigo
                Attack();
            }
            else if (distanceToTower > attackRange)
            {
                isAttacking = false; // Reseta o estado de ataque se estiver fora do alcance
                rb.velocity = direction * speed; // Move o inimigo em dire��o � torre
            }
        }
    }

    void Attack()
    {
        // Anima��o de ataque (se houver)
        Debug.Log("Inimigo atacou a torre! Dano causado: " + attackDamage);

        // Aplicar o dano � torre
        TowerHP towerHealth = TargetManager.instance.highTarget.GetComponentInParent<TowerHP>();

        if (towerHealth != null) // Verifica se a torre tem o componente TowerHP
        {
            towerHealth.TakeDamage(attackDamage); // Aplica o dano � torre
        }
        else
        {
            Debug.LogWarning("Torre n�o encontrada ou n�o tem o componente TowerHP.");
        }

        StartCoroutine(AttackCooldown());
    }
    private IEnumerator AttackCooldown()
     {
         yield return new WaitForSeconds(3f); // Tempo de espera entre os ataques
         isAttacking = false; // Permitir que o inimigo ataque novamente
     }
}