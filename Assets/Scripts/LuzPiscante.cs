using UnityEngine;
using UnityEngine.Rendering.Universal;


public class LuzPiscante : MonoBehaviour
{
    public Light2D luz;
    private bool aumentando = false;
    [SerializeField] private float velocidade;

    void Start()
    {
        if (luz == null)
            luz = GetComponent<Light2D>();
        velocidade = 4f;
    }

    void Update()
    {
        if (!aumentando)
        {
            luz.intensity -= velocidade * Time.deltaTime;
            if (luz.intensity <= 0.0f)
            {
                luz.intensity = 0.0f;
                aumentando = true;
            }
        }
        else
        {
            luz.intensity += velocidade * Time.deltaTime;
            if (luz.intensity >= 2f)
            {
                aumentando = false;
            }
        }
    }
}
