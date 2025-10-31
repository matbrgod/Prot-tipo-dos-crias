using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LuzExplosão : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Light2D luz;
    private bool aumentando = true;
    private bool diminuindo = false;
    void Start()
    {
        GetComponent<Light2D>();
        if(luz == null)
            luz = gameObject.GetComponent<Light2D>();
        luz.intensity = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (aumentando)
        {
            luz.intensity += 1f;
            if (luz.intensity >= 20f)
            {
                aumentando = false;
                diminuindo = true;
            }
        }
        if (diminuindo)
        {
            luz.intensity -= 1f;
            if (luz.intensity <= 0f)
            {
                Destroy(gameObject);
            }
        }
    }
}
