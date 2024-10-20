using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerHP : MonoBehaviour
{
    [SerializeField] public int maxHP; // Vida m�xima
    [SerializeField] public int currentHP; // Vida atual
    [SerializeField] private HealthBar healthBar;

    private void Start()
    {
        currentHP = maxHP; // Inicializa a vida atual como a vida m�xima
        Debug.Log("Torre iniciada com " + currentHP + " de vida.");
        if (healthBar != null)
        {
            healthBar.tower = this; // Passa a refer�ncia da torre para a barra de sa�de
        }
    }

    public void TakeDamage(int damageAmount) // M�todo para receber dano
    {
        currentHP -= damageAmount; // Reduz a vida atual
        Debug.Log("Torre recebeu " + damageAmount + " de dano. Vida atual: " + currentHP);

        if (healthBar != null)
        {
            healthBar.UpdateHealthBar(); // Atualiza a barra de sa�de quando a torre leva dano
        }

        if (currentHP <= 0)
        {
            DestroyTower();
        }
    }

    public void DestroyTower()
    {
        Debug.Log("A torre foi destru�da!");

        // Aqui chamamos o evento que destr�i inimigos e carrega a pr�xima cena
        GameManager.instance.OnTowerDestroyed();

        // adicionar efeitos / anima��o / evento
        Destroy(gameObject);
    }

    public void Heal(int healAmount)
    {
        currentHP += healAmount;

        // Garante que a vida da torre n�o exceda o valor m�ximo
        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }

        Debug.Log("Torre curada em " + healAmount + ". Vida atual: " + currentHP);
    }
}
