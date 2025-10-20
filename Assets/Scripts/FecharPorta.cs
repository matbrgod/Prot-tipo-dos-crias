using UnityEngine;

public class FecharPorta : MonoBehaviour
{
    public GameObject porta;
    public GameObject ratao;
    //public AudioClip closeClip;      // assign the sound in the Inspector
    //public AudioSource audioSource;  // optional: assign an AudioSource (not required)
    public GameObject musicaRatao;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter2D(Collider2D objectThatEntered)
    {
        if (objectThatEntered.CompareTag("Player"))
        {
            porta.SetActive(true);
            ratao.SetActive(true);

            musicaRatao.SetActive(true);
            
            Object.Destroy(this.gameObject);
        }
                   
    }
}

