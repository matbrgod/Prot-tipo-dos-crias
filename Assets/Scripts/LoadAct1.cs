using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadAct1 : MonoBehaviour
{
     void Start()
    {
        StartCoroutine(LoadSceneAfterDelay());
    }

    IEnumerator LoadSceneAfterDelay()
    {
        yield return new WaitForSeconds(10f);
        //SceneManager.LoadScene("Cenario 01");
        LevelManager.Instance.LoadScene("Cenario 01", "CrossFade");
    }
}
