using UnityEngine;

public class FlyEnemyHP : MonoBehaviour
{
    [SerializeField] private int maxHP; // Vida m�xima do inimigo
    [SerializeField] private int currentHP;   // Vida atual do inimigo
    [SerializeField] private GameObject deathEffect; // Efeito visual quando o inimigo � destru�do

    private void Start()
    {
        // Inicializa a vida atual como a vida m�xima
        currentHP = maxHP;
    }

    public void TakeDamage(int damageAmount)
    {
        // Reduz a vida atual
        currentHP -= damageAmount;
        Debug.Log(gameObject.name + " recebeu " + damageAmount + " de dano. Vida atual: " + currentHP);

        // Verifica se o inimigo morreu
        if (currentHP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Executa l�gica de destrui��o do inimigo
        Debug.Log(gameObject.name + " foi destru�do!");

        // Se houver um efeito de morte, instancie-o
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }

        // Destr�i o inimigo
        Destroy(gameObject);
    }
}
