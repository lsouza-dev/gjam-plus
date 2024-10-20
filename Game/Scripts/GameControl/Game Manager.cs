using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] public Transform tower;
    public UnityEvent onTowerDestroy;
    public static GameManager instance;
    public bool getIn;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
    }

    // Fun��o que ser� chamada quando a torre for destru�da
    public void OnTowerDestroyed()
    {
        onTowerDestroy.Invoke();
        DestroyAllEnemies();
        LoadNextScene();
    }
    public void OnGameOver()
    {
        DestroyAllEnemies();
        GameOver();
    }

    // Destroi todos os inimigos na cena
    void DestroyAllEnemies()
    {
        // Encontrar todos os objetos com tag "Enemy"
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy); // Destroi o inimigo
            getIn = true;
        }
    }

    // Carrega a pr�xima cena
    void LoadNextScene()
    {
        if (getIn)
        {
            SceneManager.LoadScene("HouseIn");
            getIn = false;
        }
    }
    void GameOver()
    {
        Debug.Log("GameOver");
        Time.timeScale = 0f;
        //SceneManager.LoadScene("GameOver");
    }
}
