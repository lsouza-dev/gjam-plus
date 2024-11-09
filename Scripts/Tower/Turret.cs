using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private float turretActiveHPThreshold; // Vida mínima da torre para habilitar a turret
    [SerializeField] private float usageTime; // Tempo que o jogador pode usar a turret
    [SerializeField] private float cooldownTime; // Tempo de cooldown antes de poder usar novamente
    [SerializeField] private float ySpeed; // Velocidade de movimentação da turret no eixo Y
    [SerializeField] private GameObject projectilePrefab; // Prefab do projétil para atirar
    [SerializeField] private Transform firePoint; // Ponto de disparo do projétil

    [SerializeField] private bool isActive; // Se a turret está disponível para uso
    [SerializeField] private bool isPlayerInControl; // Se o jogador está controlando a turret
    [SerializeField] private bool isInCooldown; // Se a turret está em cooldown

    private float timeLeftInUse; // Tempo restante de uso
    private float timeLeftInCooldown; // Tempo restante de cooldown

    [SerializeField] private TowerHP towerHP; // Referência ao HP da torre principal
    [SerializeField] private GameObject Player; // Referência ao jogador
    [SerializeField] private Player player;
    [SerializeField] private Animator animatorTurret;
    [SerializeField] private bool isTurretUp;
    [SerializeField] private bool isTurretDown;
    [SerializeField] private bool isTurretRight;
    CameraFollow follow;

    private Vector2 shootDirection; // Variável para armazenar a direção do tiro

    private void Awake()
    {
        follow = FindObjectOfType<CameraFollow>();
    }
    private void Start()
    {
        ActivateTurret();
    }

    private void Update()
    {
        if (isActive == true)
        {
            EnterTurret();
        }
        else if(isActive == false)
        {
            ExitTurret();
        }
        if (isPlayerInControl)
        {
            TurretControl(); // Chama o método para controlar a movimentação e o tiro da turret

            // Conta o tempo de uso
            timeLeftInUse -= Time.deltaTime;
            if (timeLeftInUse <= 0)
            {
                ExitTurret(); // Expulsa o jogador após o tempo de uso
                StartCooldown(); // Inicia o cooldown
            }
        }

        if (isInCooldown)
        {
            // Conta o tempo de cooldown
            timeLeftInCooldown -= Time.deltaTime;
            if (timeLeftInCooldown <= 0)
            {
                isInCooldown = false; // Acaba o cooldown
            }
        }
    }

    private void TurretControl()
    {
        // Controle de movimento em Y (apenas setas para cima e para baixo)
        if (Input.GetKey(KeyCode.UpArrow))
        {
            animatorTurret.Play("Up"); // Movimenta para cima
            isTurretUp = true;
            isTurretDown = false;
            isTurretRight = false;
            shootDirection = firePoint.up; // Atualiza a direção do tiro para cima
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            animatorTurret.Play("Down"); // Movimenta para baixo
            isTurretDown = true;
            isTurretUp = false;
            isTurretRight = false;
            shootDirection = -firePoint.up; // Atualiza a direção do tiro para baixo
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            animatorTurret.Play("Base");
            isTurretRight = true;
            isTurretDown = false;
            isTurretUp = false;
            shootDirection = firePoint.up;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot(); // Função de disparo
        }
    }

    private void Shoot()
    {
        // Instancia o projétil no ponto de disparo e adiciona uma força
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            float angle;
            if (isTurretUp == true)
            {
                angle = 45f;
                float radians = angle * Mathf.Deg2Rad; // Converte o ângulo para radianos

                // Calcula a direção usando o ângulo
                Vector2 shootDirection = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)).normalized;

                // Aplica a força na direção calculada
                rb.AddForce(shootDirection * 11f, ForceMode2D.Impulse); // Ajuste a força conforme necessário
            }
            else if (isTurretDown == true)
            {
                angle = -25f;
                float radians = angle * Mathf.Deg2Rad; // Converte o ângulo para radianos

                // Calcula a direção usando o ângulo
                Vector2 shootDirection = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)).normalized;

                // Aplica a força na direção calculada
                rb.AddForce(shootDirection * 14f, ForceMode2D.Impulse); // Ajuste a força conforme necessário
            }
            else if(isTurretRight == true)
            {
                angle = 5f;
                float radians = angle * Mathf.Deg2Rad; // Converte o ângulo para radianos

                // Calcula a direção usando o ângulo
                Vector2 shootDirection = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)).normalized;

                // Aplica a força na direção calculada
                rb.AddForce(shootDirection * 15f, ForceMode2D.Impulse); // Ajuste a força conforme necessário
            }
            else
            {
                angle = -25f;
                float radians = angle * Mathf.Deg2Rad; // Converte o ângulo para radianos

                // Calcula a direção usando o ângulo
                Vector2 shootDirection = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)).normalized;

                // Aplica a força na direção calculada
                rb.AddForce(shootDirection * 14f, ForceMode2D.Impulse); // Ajuste a força conforme necessário
            }
        }

        Debug.Log("Disparo realizado pela turret.");
    }

    private void ActivateTurret()
    {
        isActive = false;
    }

    private void StartCooldown()
    {
        isInCooldown = true;
        timeLeftInCooldown = cooldownTime;
        Debug.Log("A turret entrou em cooldown.");
    }

    // Função para detectar a entrada do jogador na área da turret
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Objeto entrou no Trigger: " + other.name); // Verificar se algum objeto está entrando no trigger
        if (other.CompareTag("Player") && !isActive && !isInCooldown)
        {
            Debug.Log("Press E to use");
            EnterTurret(); // Permite o jogador entrar na turret
            isActive = true;
        }
    }

    // Função chamada quando o jogador entra na área da turret
    public void EnterTurret()
    {
        
        if (isActive && !isInCooldown && Input.GetKeyDown(KeyCode.E))
        {
            isPlayerInControl = true;
            timeLeftInUse = usageTime;
            Debug.Log("O jogador está controlando a turret.");
            player.enabled = false;
            isActive = false;
        }
    }

    // Função chamada quando o jogador sai da turret ou o tempo acaba
    private void ExitTurret()
    {
        if (Input.GetKeyDown(KeyCode.E) && isPlayerInControl)
        {
            isPlayerInControl = false;
            Debug.Log("O jogador saiu da turret.");
            player.enabled = true;
        }
    }
}
