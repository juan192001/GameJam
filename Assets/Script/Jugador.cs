using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Jugador : MonoBehaviour
{
    public Transform centerObject; // Reference to the object around which the movement will be restricted
    public float radius = 5f; // Radius of the circular movement
    public float moveSpeed = 5f; // Speed at which the object moves
    public float attractionForce = 2f; // Strength of the gravitational force
    public GameObject bullet;
    public Camera mainCamera; // Reference to the main camera
    private int collisionCount = 0;
    public Sprite playerHit;
    public Sprite playerNormal;


    private void Update()
    {
        // Get input from arrow keys
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate the movement direction
        Vector2 movementDirection = new Vector2(horizontalInput, verticalInput).normalized;

        // Calculate the target position based on the movement direction and speed
        Vector2 targetPosition = (Vector2)transform.position + (movementDirection * moveSpeed * Time.deltaTime);

        // Calculate the direction from the center object to the target position
        Vector2 direction = targetPosition - (Vector2)centerObject.position;

        // If the distance between the center object and the target position is greater than the radius,
        // limit the movement to the circle's perimeter
        if (direction.magnitude > radius)
        {
            direction = direction.normalized * radius;
            targetPosition = (Vector2)centerObject.position + direction;
        }

        // Apply the gravitational force when the object is standing still
        if (movementDirection.magnitude == 0f)
        {
            Vector2 gravityDirection = ((Vector2)centerObject.position - (Vector2)transform.position).normalized;
            Vector2 gravityForce = gravityDirection * attractionForce * Time.deltaTime;
            targetPosition += gravityForce;
        }

        // Update the object's position
        transform.position = targetPosition;

       // Shoot with mouse click
        if (Input.GetMouseButtonDown(0))
        {
            // Get the mouse position in world coordinates
            Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;

            // Calculate the direction from the player to the mouse position
            Vector3 shootDirection = mousePosition - transform.position;

            // Invert the shoot direction
            shootDirection *= -1;

            // Instantiate the bullet with the calculated direction
            GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.identity);
            newBullet.GetComponent<Bullet1>().velocity = -shootDirection.normalized;
            newBullet.GetComponent<Bullet1>().speed = 5f;
            newBullet.GetComponent<Bullet1>().rotation = 0f;
            newBullet.GetComponent<Bullet1>().lifeTime = 3f;
            newBullet.GetComponent<Bullet1>().type = "Player";
            newBullet.GetComponent<PlaySound>().Play();
        }

    }

   private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Colisionando : " + collision.gameObject.name);
        if (collision.collider.CompareTag("BulletEnemigo"))
        {
            collisionCount++; // Incrementar el contador de colisiones

            if (collisionCount >= 10)
            {
                // Destruir el objeto del jugador
                Destroy(gameObject);
                SceneManager.LoadScene("Menu");
            }
            else
            {
                // Destruir la bala enemiga
                Destroy(collision.gameObject);
            }
            this.gameObject.GetComponent<SpriteRenderer>().sprite= playerHit;
            this.gameObject.GetComponent<PlaySound>().Play();
            StartCoroutine(test());

        }
    }
    IEnumerator test(){
        yield return new WaitForSeconds(2);
        this.gameObject.GetComponent<SpriteRenderer>().sprite= playerNormal;
    }

            

}
