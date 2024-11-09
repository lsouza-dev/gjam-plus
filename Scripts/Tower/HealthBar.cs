using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Transform healthBarSprite; // Refer�ncia ao Transform da barra de vida (Sprite)
    [SerializeField] public TowerHP tower; // Refer�ncia ao script da torre

    private Vector3 originalScale; // Escala original da barra de sa�de

    private void Start()
    {
        if (tower == null)
        {
            tower = GetComponentInParent<TowerHP>(); // Obt�m a torre se n�o for definido no Inspector
        }

        if (healthBarSprite != null)
        {
            originalScale = healthBarSprite.localScale; // Armazena a escala original no eixo X
        }

        // Inicializa a barra de sa�de
        UpdateHealthBar();
    }

    private void Update()
    {
        // Atualiza a barra de sa�de sempre que a vida mudar
        UpdateHealthBar();
    }

    public void UpdateHealthBar()
    {
        // Atualiza a escala da barra de sa�de com base na vida atual da torre
        if (tower != null && healthBarSprite != null)
        {
            // Calcula a porcentagem de vida
            float healthPercent = (float)tower.currentHP / tower.maxHP;

            // Atualiza a escala da barra de vida no eixo X
            healthBarSprite.localScale = new Vector3(originalScale.x * healthPercent, originalScale.y, originalScale.z);
        }
    }
}
