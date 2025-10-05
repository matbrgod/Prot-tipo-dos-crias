using UnityEngine;
using UnityEngine.SceneManagement;

public class CavernaPassado : MonoBehaviour
{
    public GameObject PortaA;
    public GameObject PortaB;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (PortaA == null)
        {
            SceneManager.LoadScene("CenaB");
            Debug.Log("PortaA destruída. Carregando CenaB.");
        }
        else if (PortaB == null)
        {
            SceneManager.LoadScene("CenaC");
            Debug.Log("PortaB destruída. Carregando CenaC.");
        }
        else
        {
            SceneManager.LoadScene("Cima2");
            Debug.Log("Collision detected with " + collision.gameObject.name);
        }
    }
}