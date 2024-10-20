using UnityEngine;

public class TargetManager : MonoBehaviour
{
    public static TargetManager instance;
    public Transform highTarget;
    public Transform downTarget;
    public Transform highTargetCheck;
    public Transform downTargetCheck;

    private bool isTowerDestroyed = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        // Verifica se a torre foi destru�da
        if (isTowerDestroyed)
        {
            return; // Se a torre foi destru�da, n�o atualiza mais o target
        }

        // Verifica se o target ainda existe
        if (highTargetCheck == null || downTarget == null)
        {
            isTowerDestroyed = true;
        }
    }

    // Fun��o para ser chamada quando a torre for destru�da
    public void OnTowerDestroyed()
    {
        isTowerDestroyed = true;
        // Opcional: Remova refer�ncias aos alvos destru�dos
        downTarget = null;
        highTarget = null;
    }
}
