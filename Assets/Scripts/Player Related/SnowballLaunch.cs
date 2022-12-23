using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowballLaunch : MonoBehaviour
{
    public AudioManagement AudioSystem;
    public AudioClip Swing;

    public float yOffset;
    public GameObject bulletPrefab; // Prefab for the bullet to be spawned
    public float fireStrength = 10f; // The strength with which the bullet will be fired
    public float bulletLifetime = 5f; // The lifetime of the bullet in seconds

    void Update()
    {
        if (Utility.instance.isGameOver || Utility.instance.runEnded || Utility.instance.isPaused || !Utility.instance.gameStarted)
            return;

        // Check if the left mouse button is being pressed
        if (Input.GetMouseButtonDown(0))
        {
            AudioSystem.PlaySFX(Swing);

            // Get the mouse position in world space
            Vector3 mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Camera.main.farClipPlane));

            // Calculate the direction and distance from the player to the mouse position
            Vector3 direction = mousePosition - transform.position;
            direction.y += yOffset;
            float distance = direction.magnitude;
            direction.Normalize();

            // Create a bullet and add force in the direction of the mouse position
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody>().AddForce(direction * fireStrength, ForceMode.Impulse);

            // Set the lifetime of the bullet
            Destroy(bullet, bulletLifetime);
        }
    }
}
