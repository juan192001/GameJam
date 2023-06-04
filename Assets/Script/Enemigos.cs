using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigos : MonoBehaviour
{
    public GameObject bulletEnemigo;
    public Transform jugador;
    public Transform centerPoint;   // Reference to the center point (sun)
    public float orbitRadius = 5f;  // Radius of the orbit

    private float orbitSpeed;       // Speed of orbit in degrees per second
    private Vector2 orbitPosition;  // Current position in the orbit
    private bool isEnemyActive = true; // Flag to determine if the enemy is active
    public float penaEn;
    private int hitCount = 0;       // Counter for number of hits

    private bool isMovementPaused = false;
    private bool isShootingPaused = false;
    private Vector2 pausePosition; // Stored position before pausing

    // Start is called before the first frame update
    void Start()
    {
        // Set a random orbit speed for each object
        orbitSpeed = Random.Range(-3f, 3f);

        // Set the initial position of the object on the orbit
        orbitPosition = CalculateOrbitPosition();
        transform.position = orbitPosition;

        // Start shooting bullets towards the player
        StartCoroutine(ShootTowardsPlayer());
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMovementPaused)
        {
            // Calculate the new position in the orbit
            orbitPosition = CalculateOrbitPosition();

            // Move the object to the new position
            transform.position = orbitPosition;
        }
    }

    private Vector2 CalculateOrbitPosition()
    {
        // Calculate the angle based on time and speed
        float angle = Time.time * orbitSpeed;

        // Calculate the position using polar coordinates
        float x = Mathf.Cos(angle) * orbitRadius;
        float y = Mathf.Sin(angle) * orbitRadius;

        // Offset the position with the center point
        Vector2 centerPosition = new Vector2(centerPoint.position.x, centerPoint.position.y);

        return centerPosition + new Vector2(x, y);
    }

    private IEnumerator ShootTowardsPlayer()
    {
        while (true)
        {
            // Check if the enemy is active
            if (isEnemyActive && !isShootingPaused)
            {
                // Wait for a certain amount of time before shooting again
                yield return new WaitForSeconds(Random.Range(1f, 3f));

                // Check if the player reference is assigned
                if (jugador != null)
                {
                    // Calculate the direction towards the player
                    Vector3 direction = (jugador.position - transform.position).normalized;

                    // Instantiate the bullet at the current position
                    GameObject newBullet = Instantiate(bulletEnemigo, transform.position, Quaternion.identity);

                    // Set the direction of the bullet towards the player
                    newBullet.GetComponent<BulletEnemigo>().SetDirection(direction);
                }
            }
            else
            {
                // Wait for a certain amount of time before resuming shooting
                yield return new WaitForSeconds(5f);

                // Resume shooting
                isShootingPaused = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Bullet"))
        {
            // Destroy the bullet
            Destroy(collision.gameObject);

            hitCount++; // Increment the hit count

            if (hitCount >= penaEn)
            {
                // Pause the enemy
                StopEnemy();
            }
        }
    }

    private void StopEnemy()
    {
        // Store the current position before pausing
        pausePosition = transform.position;

        // Stop the enemy and pause shooting and movement
        isEnemyActive = false;
        isMovementPaused = true;
        isShootingPaused = true;

        // Call the ResumeEnemy coroutine to wait for 5 seconds before resuming shooting and movement
        StartCoroutine(ResumeEnemy());
    }

    private IEnumerator ResumeEnemy()
    {
        // Wait for 5 seconds
        yield return new WaitForSeconds(5f);

        // Reset hit count
        hitCount = 0;

        // Resume shooting and movement
        isEnemyActive = true;
        isMovementPaused = false;
        isShootingPaused = false;
    }
}
