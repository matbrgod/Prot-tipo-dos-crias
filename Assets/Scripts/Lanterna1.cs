using UnityEngine;

public class Lanterna1 : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireForce = 20f;
    public void Fire()
    {
        Vector2 fireDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - firePoint.position);
        fireDirection.Normalize();
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // Get the shooter's velocity (assumes the weapon is a child of the player)
            Rigidbody2D shooterRb = GetComponentInParent<Rigidbody2D>();
            Vector2 shooterVelocity = shooterRb != null ? shooterRb.linearVelocity : Vector2.zero;

            // Set bullet velocity as shooter's velocity + fire direction
            rb.linearVelocity = shooterVelocity + fireDirection * fireForce;
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}
