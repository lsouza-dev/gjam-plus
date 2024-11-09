using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player; // Transform do jogador
    [SerializeField] private Transform turret; // Transform da turret
    [SerializeField] private Transform currentTarget; // Alvo atual que a c�mera deve seguir
    [SerializeField] private bool inTurret; // Indica se o jogador est� controlando a turret

    // Limites da c�mera em X e Y
    [SerializeField] public float minX, maxX;
    [SerializeField] public float minY, maxY;

    private void Start()
    {
        currentTarget = player; // A c�mera come�a seguindo o jogador
    }

    private void FixedUpdate()
    {
        // Atualiza o alvo atual com base no estado (se o jogador est� na turret ou n�o)
        if (inTurret)
        {
            currentTarget = turret; // A c�mera segue a turret
        }
        else
        {
            currentTarget = player; // A c�mera segue o jogador
        }

        // Pega a posi��o desejada da c�mera (usando Lerp para suavizar)
        Vector3 desiredPosition = Vector2.Lerp(transform.position, currentTarget.position, 0.1f);

        // Limita a posi��o desejada dentro dos limites
        float clampedX = Mathf.Clamp(desiredPosition.x, minX, maxX);
        float clampedY = Mathf.Clamp(desiredPosition.y, minY, maxY);

        // Aplica a posi��o limitada � c�mera
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }

    // Fun��o para ativar a c�mera na turret
    public void EnterTurret()
    {
        inTurret = true; // Ativa o controle da turret
    }

    // Fun��o para voltar a seguir o jogador
    public void ExitTurret()
    {
        inTurret = false; // Volta a seguir o jogador
    }
}
