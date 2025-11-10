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
                //SceneManager.LoadScene("Futuro 1");
                LevelManager.Instance.LoadScene("Futuro 1", "CrossFade");
            }
            if (Futuro2 == true)
            {
                //SceneManager.LoadScene("Futuro 2");
                LevelManager.Instance.LoadScene("Futuro 2", "CrossFade");
            }
            if (Futuro3 == true)
            {
                //SceneManager.LoadScene("Futuro 3");
                LevelManager.Instance.LoadScene("Futuro 3", "CrossFade");
            }
            if (Futuro4 == true)
            {
                //SceneManager.LoadScene("Futuro 4");
                LevelManager.Instance.LoadScene("Futuro 4", "CrossFade");
            }
        }
    }
}
