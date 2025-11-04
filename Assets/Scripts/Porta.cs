using UnityEngine;
using UnityEngine.SceneManagement;

public class Porta : MonoBehaviour
{
    public bool Futuro1;
    public bool Futuro2;
    public bool Futuro3;
    public bool Futuro4;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Futuro1 == true)
            {
                SceneManager.LoadScene("Futuro 1");
            }
            if (Futuro2 == true)
            {
                SceneManager.LoadScene("Futuro 2");
            }
            if (Futuro3 == true)
            {
                SceneManager.LoadScene("Futuro 3");
            }
            if (Futuro4 == true)
            {
                SceneManager.LoadScene("Futuro 4");
            }
        }
    }
}
