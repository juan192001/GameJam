using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemigo : MonoBehaviour
{
    public float speed = 5f; // Speed of the bullet
    private Vector3 direction; // Direction of the bullet
    public GameObject hitEffect;

    // Update is called once per frame
    void Update()
    {
        // Move the bullet in the specified direction and speed
        transform.Translate(direction * speed * Time.deltaTime);
    }

    public void SetDirection(Vector3 dir)
    {
        // Set the direction of the bullet
        direction = dir;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the bullet collides with the player
        if (collision.collider.CompareTag("Player"))
        {
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, 0.5f);

        // Destroy the bullet
            Destroy(gameObject);
            
            // TODO: Add any additional logic for when the bullet hits the player
        }
    }
}
