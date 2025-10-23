using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject Impacto;
    public GameObject rastro;
    private SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Impacto.SetActive(false);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            return;
        }
        else
        {
            rastro.SetActive(false);
            Impacto.SetActive(true);
            this.spriteRenderer.enabled = false;
            StartCoroutine(DestroyBulletAfterImpact());

        }

    }
    private IEnumerator DestroyBulletAfterImpact()
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    } 
}
