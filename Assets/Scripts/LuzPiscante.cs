using UnityEngine;
using UnityEngine.Rendering.Universal;


public class LuzPiscante : MonoBehaviour
{
    public Light2D luz;
    private bool aumentando = false;

    void Start()
    {
        if (luz == null)
            luz = GetComponent<Light2D>();
    }

    void Update()
    {
        if (!aumentando)
        {
            luz.intensity -= 1f * Time.deltaTime;
            if (luz.intensity <= 0.0f)
            {
                luz.intensity = 0.0f;
                aumentando = true;
            }
        }
        else
        {
            luz.intensity += 1f * Time.deltaTime;
            if (luz.intensity >= 2f)
            {
                aumentando = false;
            }
        }
    }
}
