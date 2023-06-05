using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet1 : MonoBehaviour
{
    public Vector2 velocity;
    public float speed;
    public float rotation;
    public float lifeTime;
    public string type;
    public float timer;
    public GameObject hitEffect;

    void Start()
    {
        timer = lifeTime;
        transform.rotation = Quaternion.Euler(0, 0, rotation);
    }

    void Update()
    {
        transform.Translate(velocity * speed * Time.deltaTime);
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemigo"))
        {
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, 0.5f);

        // Destroy the bullet
            Destroy(gameObject);
            Destroy(gameObject);
        }
    }
}
