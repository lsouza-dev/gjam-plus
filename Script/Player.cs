using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [SerializeField] private bool isAlive = true;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private GameObject bulletSpawner;
    [SerializeField] private Bullet bullet;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float xSpawnerPos;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            Movement();
            Shooting();
        }
    }

    private void Movement()
    {
        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");

        if (xInput > 0)
        {
            rb.velocity = Vector2.right * speed;
            bulletSpawner.transform.position = new Vector2(transform.position.x + 2f, transform.position.y);
            bullet.direction = 0;
        }
        else if (xInput < 0)
        {
            rb.velocity = Vector2.left * speed;
            bulletSpawner.transform.position = new Vector2(transform.position.x - 2f, transform.position.y);
            bullet.direction = 1;
        }

        if (yInput > 0)
        {
            bulletSpawner.transform.position = new Vector2(transform.position.x, transform.position.y + 2f);
            bullet.direction = 2;
            //  rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }
        else if(yInput < 0)
        {
            bulletSpawner.transform.position = new Vector2(transform.position.x, transform.position.y - 2f);
            bullet.direction = 3;
        }
        
    }

    private void Shooting()
    {
        if (isAlive && Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bullet,bulletSpawner.transform.position,Quaternion.identity);
        }
    }
}
