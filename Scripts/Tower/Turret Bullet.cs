using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBullet : MonoBehaviour
{
    [SerializeField] private float lifetime;

    private void Start()
    {
        // Destr�i o proj�til ap�s 2 segundos (ou o valor definido em lifetime)
        Destroy(gameObject, lifetime);
    }
}
