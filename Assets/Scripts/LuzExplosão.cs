using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LuzExplosão : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Light2D luz;
    private bool aumentando = true;
    private bool diminuindo = false;
    public float tamanhoMaximo = 30f;
    void Start()
    {
        GetComponent<Light2D>();
        if(luz == null)
            luz = gameObject.GetComponent<Light2D>();
        luz.intensity = 0f;
        if (tamanhoMaximo < 0.1f)
            tamanhoMaximo = 30f;
    }

    // Update is called once per frame
    void Update()
    {
        if (aumentando)
        {
            luz.intensity += 1f;
            luz.pointLightOuterRadius += 1f;
            if (luz.pointLightOuterRadius >= tamanhoMaximo)
            {
                aumentando = false;
                diminuindo = true;
            }
        }
        if (diminuindo)
        {
            luz.intensity -= 1f;
            luz.pointLightOuterRadius -= 1f;
            if (luz.pointLightOuterRadius <= 0f)
            {
                Destroy(gameObject);
            }
        }
    }
}
