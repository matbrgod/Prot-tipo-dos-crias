using UnityEngine;

public class TrocarArma : MonoBehaviour
{
    public GameObject childA;
    public GameObject childB;

    // Tecla para alternar
    public KeyCode toggleKey = KeyCode.Space;

    private bool isAActive = true;

    void Start()
    {
        // Garantir estado inicial
        childA.SetActive(true);
        childB.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            Toggle();
        }
    }

    void Toggle()
    {
        isAActive = !isAActive;

        childA.SetActive(isAActive);
        childB.SetActive(!isAActive);
    }
}
