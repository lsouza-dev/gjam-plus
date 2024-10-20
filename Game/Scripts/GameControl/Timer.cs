using UnityEngine;
using TMPro; // Certifique-se de incluir este namespace

public class GameTimer : MonoBehaviour
{
    [SerializeField] private float totalTime = 60f; // Tempo total em segundos
    [SerializeField] private TextMeshProUGUI timerText; // Use TextMeshProUGUI ao invés de Text
    [SerializeField] private bool isPaused;

    [SerializeField] private float timeRemaining;

    private void Start()
    {
        timeRemaining = totalTime; // Inicializa o tempo restante
        UpdateTimerText();
    }

    private void Update()
    {
        if (!isPaused && timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerText();
        }
        else
        {
            timeRemaining = 0;
            TimeIsUp();
        }
    }

    private void UpdateTimerText()
    {
        // Calcula minutos e segundos restantes
        int minutes = Mathf.FloorToInt(timeRemaining / 60); // Obtém o número de minutos
        int seconds = Mathf.FloorToInt(timeRemaining % 60); // Obtém o número de segundos restantes

        // Formata a string para mostrar os minutos e segundos
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void TimeIsUp()
    {
        // Lógica quando o tempo acabar
        Debug.Log("O tempo acabou!");
    }
}
