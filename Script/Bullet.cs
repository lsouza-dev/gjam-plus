using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Vector2 dir;
    [SerializeField] public int direction;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        switch (direction)
        {
            case 0:
                dir = new Vector2(bulletSpeed, 0);
                break;
            case 1:
                dir = new Vector2(-bulletSpeed, 0);
                break;
            case 2:
                dir = new Vector2(0,bulletSpeed);
                break;
            case 3:
                dir = new Vector2(0, -bulletSpeed);
                break;
        }

        rb.velocity = dir;

        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
