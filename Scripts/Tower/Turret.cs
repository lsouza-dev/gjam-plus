using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private float turretActiveHPThreshold; // Vida m�nima da torre para habilitar a turret
    [SerializeField] private float usageTime; // Tempo que o jogador pode usar a turret
    [SerializeField] private float cooldownTime; // Tempo de cooldown antes de poder usar novamente
    [SerializeField] private float ySpeed; // Velocidade de movimenta��o da turret no eixo Y
    [SerializeField] private GameObject projectilePrefab; // Prefab do proj�til para atirar
    [SerializeField] private Transform firePoint; // Ponto de disparo do proj�til

    [SerializeField] private bool isActive; // Se a turret est� dispon�vel para uso
    [SerializeField] private bool isPlayerInControl; // Se o jogador est� controlando a turret
    [SerializeField] private bool isInCooldown; // Se a turret est� em cooldown

    private float timeLeftInUse; // Tempo restante de uso
    private float timeLeftInCooldown; // Tempo restante de cooldown

    [SerializeField] private TowerHP towerHP; // Refer�ncia ao HP da torre principal
    [SerializeField] private GameObject Player; // Refer�ncia ao jogador
    [SerializeField] private Player player;
    [SerializeField] private Animator animatorTurret;
    [SerializeField] private bool isTurretUp;
    [SerializeField] private bool isTurretDown;
    [SerializeField] private bool isTurretRight;
    CameraFollow follow;

    private Vector2 shootDirection; // Vari�vel para armazenar a dire��o do tiro

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
            TurretControl(); // Chama o m�todo para controlar a movimenta��o e o tiro da turret

            // Conta o tempo de uso
            timeLeftInUse -= Time.deltaTime;
            if (timeLeftInUse <= 0)
            {
                ExitTurret(); // Expulsa o jogador ap�s o tempo de uso
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
            shootDirection = firePoint.up; // Atualiza a dire��o do tiro para cima
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            animatorTurret.Play("Down"); // Movimenta para baixo
            isTurretDown = true;
            isTurretUp = false;
            isTurretRight = false;
            shootDirection = -firePoint.up; // Atualiza a dire��o do tiro para baixo
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
            Shoot(); // Fun��o de disparo
        }
    }

    private void Shoot()
    {
        // Instancia o proj�til no ponto de disparo e adiciona uma for�a
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            float angle;
            if (isTurretUp == true)
            {
                angle = 45f;
                float radians = angle * Mathf.Deg2Rad; // Converte o �ngulo para radianos

                // Calcula a dire��o usando o �ngulo
                Vector2 shootDirection = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)).normalized;

                // Aplica a for�a na dire��o calculada
                rb.AddForce(shootDirection * 11f, ForceMode2D.Impulse); // Ajuste a for�a conforme necess�rio
            }
            else if (isTurretDown == true)
            {
                angle = -25f;
                float radians = angle * Mathf.Deg2Rad; // Converte o �ngulo para radianos

                // Calcula a dire��o usando o �ngulo
                Vector2 shootDirection = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)).normalized;

                // Aplica a for�a na dire��o calculada
                rb.AddForce(shootDirection * 14f, ForceMode2D.Impulse); // Ajuste a for�a conforme necess�rio
            }
            else if(isTurretRight == true)
            {
                angle = 5f;
                float radians = angle * Mathf.Deg2Rad; // Converte o �ngulo para radianos

                // Calcula a dire��o usando o �ngulo
                Vector2 shootDirection = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)).normalized;

                // Aplica a for�a na dire��o calculada
                rb.AddForce(shootDirection * 15f, ForceMode2D.Impulse); // Ajuste a for�a conforme necess�rio
            }
            else
            {
                angle = -25f;
                float radians = angle * Mathf.Deg2Rad; // Converte o �ngulo para radianos

                // Calcula a dire��o usando o �ngulo
                Vector2 shootDirection = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)).normalized;

                // Aplica a for�a na dire��o calculada
                rb.AddForce(shootDirection * 14f, ForceMode2D.Impulse); // Ajuste a for�a conforme necess�rio
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

    // Fun��o para detectar a entrada do jogador na �rea da turret
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Objeto entrou no Trigger: " + other.name); // Verificar se algum objeto est� entrando no trigger
        if (other.CompareTag("Player") && !isActive && !isInCooldown)
        {
            Debug.Log("Press E to use");
            EnterTurret(); // Permite o jogador entrar na turret
            isActive = true;
        }
    }

    // Fun��o chamada quando o jogador entra na �rea da turret
    public void EnterTurret()
    {
        
        if (isActive && !isInCooldown && Input.GetKeyDown(KeyCode.E))
        {
            isPlayerInControl = true;
            timeLeftInUse = usageTime;
            Debug.Log("O jogador est� controlando a turret.");
            player.enabled = false;
            isActive = false;
        }
    }

    // Fun��o chamada quando o jogador sai da turret ou o tempo acaba
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
