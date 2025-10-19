using UnityEngine;

public class FecharPorta : MonoBehaviour
{
    public GameObject porta;
    private Player player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter2D(Collider2D objectThatEntered)
    {
        if (objectThatEntered.CompareTag("Player"))
        {
            player = objectThatEntered.GetComponent<Player>();
        }
        //Instantiate(porta, transform.position, Quaternion.identity); 
        porta.SetActive(true);   
        Object.Destroy(this.gameObject);   
    }
}

