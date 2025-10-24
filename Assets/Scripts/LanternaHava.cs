using UnityEngine;

public class LanternaHava : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject lanterna;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            lanterna.SetActive(!lanterna.activeSelf);
        }
    }
}
