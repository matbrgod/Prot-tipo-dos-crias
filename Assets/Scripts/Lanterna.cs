using UnityEngine;

public class Lanterna : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject bulletPrefab;
    public Transform firePoint1;
    
    public void Fire()
    {
        Vector2 fireDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - firePoint1.position).normalized;
        
        
        
    }
    // Update is called once per frame
    void Update()
    {

    }
}
