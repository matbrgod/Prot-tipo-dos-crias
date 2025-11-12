using UnityEngine;

public class FecharPorta : MonoBehaviour
{
    public GameObject porta;
    public GameObject ratao;
    [SerializeField] private GameObject quest1;
    //public AudioClip closeClip;      // assign the sound in the Inspector
    //public AudioSource audioSource;  // optional: assign an AudioSource (not required)
    public AudioSource musicaRatao;
    public AudioSource musicaAmbiente;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter2D(Collider2D objectThatEntered)
    {
        if (objectThatEntered.CompareTag("Player"))
        {
            porta.SetActive(true);
            ratao.SetActive(true);
            quest1.SetActive(false);

            MusicManager.Instance.PlayMusic("MiniBoss");
            //musicaAmbiente.Stop();
            
            Object.Destroy(this.gameObject);
        }
                   
    }
}

