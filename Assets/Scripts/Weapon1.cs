using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireForce = 20f;
    public AudioSource fireSound;

    // new: adjustable pitch range
    [Range(0.5f, 2f)] public float firePitchMin = 0.95f;
    [Range(0.5f, 2f)] public float firePitchMax = 1.05f;

    public void Start()
    {
        AudioSource audioSource = GetComponent<AudioSource>();    
    }

    public void Fire()
    {
        // randomize pitch to reduce repetitiveness
        if (fireSound != null)
        {
            fireSound.pitch = Random.Range(firePitchMin, firePitchMax);
            fireSound.Play();
        }

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation * Quaternion.Euler(0, 0, 90));
        var rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null) rb.AddForce(firePoint.up * fireForce, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {

    }
}