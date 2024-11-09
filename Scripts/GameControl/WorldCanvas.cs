using UnityEngine;

public class FollowGameObject : MonoBehaviour
{
    [SerializeField] private Transform target; // O objeto que o Canvas seguir�
    [SerializeField] private Canvas worldSpaceCanvas; // Refer�ncia ao Canvas em World Space

    private void Update()
    {
        // Atualiza a posi��o do Canvas para a posi��o do GameObject alvo
        if (worldSpaceCanvas != null && target != null)
        {
            worldSpaceCanvas.transform.position = target.position;
        }
    }
}