using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour
{
    public GameObject menuCanvas;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.1f);
        menuCanvas.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {   
        if(Input.GetKeyDown(KeyCode.Q))
        {
            menuCanvas.SetActive(!menuCanvas.activeSelf);
        }
    }
}