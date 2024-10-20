using UnityEngine;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    [System.Serializable] // Para exibir no Inspector do Unity
    public class SpawnPoint
    {
        public Transform spawnLocation; // Local espec�fico para spawnar o inimigo
        public GameObject enemyPrefab; // Tipo de inimigo a ser spawnado
        public float spawnDelay; // Tempo entre os spawns para este ponto espec�fico
        public float startSpawnDelay; // Tempo antes de come�ar a spawnar
        [HideInInspector] public float nextSpawnTime; // Armazena o pr�ximo tempo de spawn para este ponto

        public void Initialize()
        {
            // Define o pr�ximo tempo de spawn considerando o tempo de in�cio
            nextSpawnTime = Time.time + startSpawnDelay; // O spawn s� acontecer� ap�s o startDelay
        }
    }

    [SerializeField] private List<SpawnPoint> spawnPoints; // Lista de pontos de spawn
    [SerializeField] private Transform towerTransform; // Refer�ncia � posi��o da torre no cen�rio

    void Start()
    {
        // Inicializa o pr�ximo tempo de spawn para cada ponto de spawn
        foreach (var spawnPoint in spawnPoints)
        {
            spawnPoint.Initialize(); // Chama o m�todo de inicializa��o
        }
    }

    void Update()
    {
        foreach (var spawnPoint in spawnPoints)
        {
            // Apenas spawnar inimigos se o tempo atual for maior ou igual ao pr�ximo tempo de spawn
            if (Time.time >= spawnPoint.nextSpawnTime)
            {
                SpawnEnemy(spawnPoint);
                spawnPoint.nextSpawnTime = Time.time + spawnPoint.spawnDelay; // Atualiza o pr�ximo tempo de spawn
            }
        }
    }

    void SpawnEnemy(SpawnPoint chosenPoint)
    {
        if (towerTransform != null)
        {
            // Instancia o inimigo no ponto de spawn escolhido
            GameObject newEnemy = Instantiate(chosenPoint.enemyPrefab, chosenPoint.spawnLocation.position, Quaternion.identity);

            // Calcula a dire��o entre o inimigo e a torre
            float directionToTower = towerTransform.position.x - chosenPoint.spawnLocation.position.x;

            // Se a dire��o for positiva (torre � direita), o inimigo mant�m sua escala normal
            // Se a dire��o for negativa (torre � esquerda), o inimigo inverte a escala no eixo X
            Vector3 enemyScale = newEnemy.transform.localScale;
            if (directionToTower < 0)
            {
                // Torre � direita, mant�m a escala
                enemyScale.x = Mathf.Abs(enemyScale.x); // Garante que X seja positivo
            }
            else
            {
                // Torre � esquerda, inverte a escala
                enemyScale.x = -Mathf.Abs(enemyScale.x); // Garante que X seja negativo
            }

            // Aplica a nova escala ao inimigo
            newEnemy.transform.localScale = enemyScale;
        }
    }
}